using UnityEngine;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
    public Slider brightnessSlider;     // arrastrar el slider
    public Image panelBrilloImage;      // panel negro (Image) cuyo color alpha controlas
    private const string PREF_KEY = "brightness_value";

    void Start()
    {
        // leer valor guardado (si no existe, 0.5 por defecto)
        float saved = PlayerPrefs.GetFloat(PREF_KEY, 0.5f);
        // el tutorial recomienda max 0.9 si no quieres pantalla totalmente negra
        brightnessSlider.maxValue = 0.9f;
        brightnessSlider.minValue = 0f;
        brightnessSlider.value = saved;

        ApplyAlpha(saved);

        // suscribirse al slider (también puedes hacerlo desde el inspector con OnValueChanged)
        brightnessSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    public void OnSliderChanged(float val)
    {
        ApplyAlpha(val);
        PlayerPrefs.SetFloat(PREF_KEY, val);
        PlayerPrefs.Save();
    }

    void ApplyAlpha(float alpha)
    {
        Color c = panelBrilloImage.color;
        panelBrilloImage.color = new Color(c.r, c.g, c.b, alpha);
    }

    void OnDestroy()
    {
        brightnessSlider.onValueChanged.RemoveListener(OnSliderChanged);
    }
}
