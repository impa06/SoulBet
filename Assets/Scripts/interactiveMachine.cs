using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractMachine : MonoBehaviour
{
    public GameObject InteractText; // Texto "Presiona E"
    public string slotMachine;      // Nombre de la escena
    bool playerInside = false;

    void Start()
    {
        if (InteractText != null)
            InteractText.SetActive(false); // el texto inicia oculto
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entró al área de interacción");
            playerInside = true;

            if (InteractText != null)
                InteractText.SetActive(true); // muestra el texto
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player salió del área de interacción");
            playerInside = false;

            if (InteractText != null)
                InteractText.SetActive(false); // oculta el texto
        }
    }

    void Update()
    {
        // Solo permite interactuar si:
        // 1) El jugador está dentro del trigger
        // 2) El texto está visible
        if (playerInside && InteractText.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Presionó E cerca de la máquina, cargando escena...");
                SceneManager.LoadScene(slotMachine);
            }
        }
    }
}
