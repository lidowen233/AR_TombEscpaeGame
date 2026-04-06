using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FadeAway : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private Image image;

    public float fadeAfterXSeconds;
    public float fadeDuration;

    // Start is called before the first frame update
    void Awake()
    {
        if (text != null)
        {
            StartCoroutine(FadeTextAway());
        }
        if (image != null)
        {
            StartCoroutine(FadeImageAway());
        }
    }

    private IEnumerator FadeImageAway()
    {
        Color initialColor = image.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        float elapsedTime = fadeAfterXSeconds * -1.0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            image.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }

    }


    private IEnumerator FadeTextAway()
    {
        Color initialColor = text.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        float elapsedTime = fadeAfterXSeconds * -1.0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            text.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }

    }

}
