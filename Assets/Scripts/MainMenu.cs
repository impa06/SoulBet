using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject Menu; // panel principal

    private const string PREF_KEY = "SeenCinematic";

    public void OpenOptionsPanel()
    {
        Menu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OpenMainMenuPanel()
    {
        Menu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Asociar este método al botón "Jugar"
    public void StartGame()
    {
        // Si ya vio la cinemática -> carga directamente la escena de juego
        if (PlayerPrefs.GetInt(PREF_KEY, 0) == 1)
        {
            SceneManager.LoadScene("SampleScene");
            return;
        }

        // Si no la vio -> cargar la escena de la cinemática
        SceneManager.LoadScene("SceneCinematic");
    }
}
