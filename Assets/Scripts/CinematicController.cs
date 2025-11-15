using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    [TextArea] public string text;
    public float minDisplaySeconds = 1.2f; // tiempo mínimo antes de poder avanzar
}

public class CinematicController : MonoBehaviour
{
    [Header("Escenas")]
    [SerializeField] private string gameSceneName = "SampleScene";

    [Header("Personajes (asigna las imágenes)")]
    public Image characterAImage; // personaje izquierdo
    public Image characterBImage; // personaje derecho

    [Header("Texto (asigna TMP o Text)")]
    public TextMeshProUGUI nameTMP;
    public TMPro.TextMeshProUGUI dialogueTMP;
    public Text nameUI;
    public Text dialogueUI;

    [Header("Typewriter")]
    public TypewriterText typewriter; // asocia el componente TypewriterText (si usas TMP asigna su tmpText)

    [Header("Líneas de diálogo")]
    public List<DialogueLine> lines = new List<DialogueLine>();

    [Header("Opciones")]
    public bool allowClickToAdvance = true;
    public KeyCode skipKey = KeyCode.Space;
    public float extraPauseBetweenLines = 0.12f;

    private const string PREF_KEY = "SeenCinematic";
    private Coroutine playRoutine;
    private bool isSkippingAll = false;

    private void Start()
    {
        // ocultar personajes hasta que hablen
        SetCharactersActive(false, false);
        ClearDialogue();
        playRoutine = StartCoroutine(PlayCinematic());
    }

    private void Update()
    {
        // si presiona skipKey: salta toda la cinemática
        if (Input.GetKeyDown(skipKey))
        {
            isSkippingAll = true;
            SkipAllAndLoadGame();
        }
    }

    IEnumerator PlayCinematic()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            DialogueLine dl = lines[i];

            // Decide qué personaje mostrar (básico por nombre)
            string lower = dl.speakerName.ToLower();
            if (lower.Contains("a") || lower.Contains("personaje a") || lower.Contains("personajea"))
                SetCharactersActive(true, false);
            else if (lower.Contains("b") || lower.Contains("personaje b") || lower.Contains("personajeb"))
                SetCharactersActive(false, true);
            else
                SetCharactersActive(true, true);

            // colocar nombre
            if (nameTMP != null) nameTMP.text = dl.speakerName;
            if (nameUI != null) nameUI.text = dl.speakerName;

            // Mostrar diálogo con typewriter
            if (typewriter != null)
            {
                // config para typewriter: si usas TMP, asegúrate de haber asignado typewriter.tmpText = dialogueTMP en Inspector
                typewriter.StopAllReveals();
                Coroutine reveal = typewriter.Reveal(dl.text);

                bool revealFinished = false;
                // comprobamos si el coroutine ha terminado revisando texto completo
                while (!revealFinished)
                {
                    // si usuario hace click y permitimos avance: muestra texto completo inmediatamente
                    if (allowClickToAdvance && Input.GetMouseButtonDown(0))
                    {
                        // forzar texto completo
                        typewriter.StopAllReveals();
                        typewriter.SetImmediate(dl.text);
                        revealFinished = true;
                        break;
                    }

                    // si se ha escrito todo (comparamos texto)
                    string currentText = (typewriter.tmpText != null) ? typewriter.tmpText.text : typewriter.uiText != null ? typewriter.uiText.text : "";
                    if (currentText == dl.text) revealFinished = true;

                    // salto total
                    if (isSkippingAll) yield break;

                    yield return null;
                }
            }
            else
            {
                // sin typewriter: muestra el texto directamente
                if (dialogueTMP != null) dialogueTMP.text = dl.text;
                if (dialogueUI != null) dialogueUI.text = dl.text;
            }

            // ahora esperar el tiempo mínimo o espera click para avanzar
            float waited = 0f;
            bool clicked = false;
            while (waited < dl.minDisplaySeconds && !clicked)
            {
                if (allowClickToAdvance && Input.GetMouseButtonDown(0))
                {
                    clicked = true;
                    break;
                }
                if (isSkippingAll) yield break;
                waited += Time.deltaTime;
                yield return null;
            }

            // si el usuario hace click después del minDisplaySeconds, lo detectamos aquí también
            while (allowClickToAdvance && Input.GetMouseButtonDown(0) == false && !isSkippingAll && waited >= dl.minDisplaySeconds)
            {
                // dar pequeña ventana para que el usuario haga click para acelerar
                // si no quiere esperar, seguimos tras extraPauseBetweenLines
                break;
            }

            yield return new WaitForSeconds(extraPauseBetweenLines);
        }

        // Fin de cinemática -> guardar y cargar escena
        if (!isSkippingAll)
        {
            PlayerPrefs.SetInt(PREF_KEY, 1);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(gameSceneName);
    }

    private void SetCharactersActive(bool a, bool b)
    {
        if (characterAImage != null) characterAImage.gameObject.SetActive(a);
        if (characterBImage != null) characterBImage.gameObject.SetActive(b);
    }

    private void ClearDialogue()
    {
        if (nameTMP != null) nameTMP.text = "";
        if (dialogueTMP != null) dialogueTMP.text = "";
        if (nameUI != null) nameUI.text = "";
        if (dialogueUI != null) dialogueUI.text = "";
    }

    private void SkipAllAndLoadGame()
    {
        PlayerPrefs.SetInt(PREF_KEY, 1);
        PlayerPrefs.Save();
        if (playRoutine != null) StopCoroutine(playRoutine);
        SceneManager.LoadScene(gameSceneName);
    }
}
