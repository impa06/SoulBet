using System.Collections;
using UnityEngine;
#if TMP_PRESENT
using TMPro;
#endif
using UnityEngine.UI;

public class TypewriterText : MonoBehaviour
{
    [Header("Referencias (asigna solo la que uses)")]
    public UnityEngine.UI.Text uiText; // asigna si usas UI Text
    public TMPro.TextMeshProUGUI tmpText; // asigna si usas TextMeshProUGUI

    [Header("Velocidad")]
    public float charsPerSecond = 40f;

    public Coroutine Reveal(string fullText)
    {
        return StartCoroutine(RevealCoroutine(fullText));
    }

    private IEnumerator RevealCoroutine(string fullText)
    {
        if (uiText == null && tmpText == null)
            yield break;

        if (charsPerSecond <= 0) charsPerSecond = 40f;
        float delay = 1f / charsPerSecond;

        int len = fullText.Length;
        for (int i = 0; i <= len; i++)
        {
            string sub = fullText.Substring(0, i);
            if (uiText != null) uiText.text = sub;
            if (tmpText != null) tmpText.text = sub;
            yield return new WaitForSeconds(delay);
        }
    }

    // cancelar cualquier reveal en curso
    public void StopAllReveals()
    {
        StopAllCoroutines();
    }

    // establecer texto inmediatamente (sin typewriter)
    public void SetImmediate(string text)
    {
        if (uiText != null) uiText.text = text;
        if (tmpText != null) tmpText.text = text;
    }
}
