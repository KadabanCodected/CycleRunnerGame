using UnityEngine;
using System.Collections;

public class QuizTrigger : MonoBehaviour
{
    [Header("UI")]
    public GameObject quizCanvas;
    public GameObject speedPanel;

    [Header("Music Settings")]
    public AudioSource musicSource;
    [SerializeField] private float dimmedVolume = 0.2f;
    [SerializeField] private float fadeDuration = 1f;

    private float originalVolume;
    private bool triggered = false;

    private void Start()
    {
        if (musicSource != null)
        {
            originalVolume = musicSource.volume;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            if (quizCanvas != null)
            {
                quizCanvas.SetActive(true);
                Time.timeScale = 0f;
            }

            if (speedPanel != null)
                speedPanel.SetActive(false);

            if (musicSource != null && musicSource.volume > 0f)
            {
                originalVolume = musicSource.volume;
                StartCoroutine(FadeMusic(musicSource, dimmedVolume));
            }

            triggered = true;
            GetComponent<Collider>().enabled = false;
        }
    }

    public void RestoreMusic()
    {
        if (musicSource != null && originalVolume > 0f)
            StartCoroutine(FadeMusic(musicSource, originalVolume));
    }

    private IEnumerator FadeMusic(AudioSource source, float targetVolume)
    {
        float startVolume = source.volume;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, t / fadeDuration);
            yield return null;
        }

        source.volume = targetVolume;
    }
}
