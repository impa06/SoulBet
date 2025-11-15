using UnityEngine;
using UnityEngine.UI;

public class BrightnessApplier : MonoBehaviour
{
    public Image panelBrilloImage; // arrastra el panel brillo de la escena2
    private const string PREF_KEY = "brightness_value";

    void Start()
    {
        float saved = PlayerPrefs.GetFloat(PREF_KEY, 0.5f);
        Color c = panelBrilloImage.color;
        panelBrilloImage.color = new Color(c.r, c.g, c.b, saved);
    }
}
