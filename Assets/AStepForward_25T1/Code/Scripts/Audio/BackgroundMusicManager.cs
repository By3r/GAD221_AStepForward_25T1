using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    #region Variables
    public static BackgroundMusicManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private float fadeSpeed = 2f;
    [SerializeField] private float loweredVolume = 0.1f;
    private float _originalVolume;
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (musicSource != null)
            _originalVolume = musicSource.volume;
    }

    public void FadeOutMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeVolume(musicSource.volume, loweredVolume));
    }

    public void FadeInMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeVolume(musicSource.volume, _originalVolume));
    }

    private System.Collections.IEnumerator FadeVolume(float from, float to)
    {
        float timer = 0f;
        while (Mathf.Abs(musicSource.volume - to) > 0.01f)
        {
            timer += Time.deltaTime * fadeSpeed;
            musicSource.volume = Mathf.Lerp(from, to, timer);
            yield return null;
        }
        musicSource.volume = to;
    }
}