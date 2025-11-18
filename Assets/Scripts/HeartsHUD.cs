using UnityEngine;
using UnityEngine.UI;

public class HeartsHUD : MonoBehaviour
{
    public static HeartsHUD instance;

    public Image[] hearts; // arrastra tus 3 imágenes de corazón

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateHearts();
    }

    public void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = (i < PlayerStats.instance.lives);
        }
    }
}
