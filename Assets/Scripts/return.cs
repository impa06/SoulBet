using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButton : MonoBehaviour
{
    public string SampleScene; // ← Nombre de la escena a la que quieres volver

    public void ReturnToScene()
    {
        SceneManager.LoadScene(SampleScene);
    }
}
