using UnityEngine;
using UnityEngine.SceneManagement;

public class SlotMachineProximity2D : MonoBehaviour
{
    [Header("UI")]
    public GameObject pressEText;     // Texto "Presiona E"

    [Header("Scene")]
    public string sceneName = "slotMachine";

    bool playerNearby = false;

    private void Start()
    {
        if (pressEText != null)
            pressEText.SetActive(false);     // ocultar al inicio
    }

    private void Update()
    {
        if (!playerNearby) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerNearby = true;

        if (pressEText != null)
            pressEText.SetActive(true);      // Mostrar "Presiona E"
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerNearby = false;

        if (pressEText != null)
            pressEText.SetActive(false);     // Ocultar texto
    }
}
