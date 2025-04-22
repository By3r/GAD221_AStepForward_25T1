using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    #region Variables
    public static FadeManager Instance;

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.5f;
    #endregion

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator FadeOut()
    {
        yield return StartCoroutine(Fade(0f, 1f));
    }

    public IEnumerator FadeIn()
    {
        yield return StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float from, float to)
    {
        float timer = 0f;
        Color c = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, timer / fadeDuration);
            fadeImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(c.r, c.g, c.b, to);
    }
}