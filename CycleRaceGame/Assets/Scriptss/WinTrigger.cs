using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject speedPanel;
    [SerializeField] private float fadeDuration = 1f;

    [Header("Music Settings")]
    [SerializeField] private AudioSource musicSource;

    private CanvasGroup canvasGroup;
    private bool isWin = false;

    private const string VolumeKey = "MusicVolume";

    private void Start()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false);
            canvasGroup = winPanel.GetComponent<CanvasGroup>();

            if (canvasGroup == null)
                canvasGroup = winPanel.AddComponent<CanvasGroup>();

            canvasGroup.alpha = 0f;
        }

        // при старте выставляем громкость из сохранённых настроек
        if (musicSource != null)
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
            musicSource.volume = savedVolume;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isWin && winPanel != null)
        {
            isWin = true;

            if (speedPanel != null)
                speedPanel.SetActive(false);

            Time.timeScale = 0f;

            winPanel.SetActive(true);
            StartCoroutine(FadeIn());

            // если музыка реально играет (громкость > 0) → приглушаем
            if (musicSource != null && musicSource.volume > 0f)
                StartCoroutine(FadeOutMusic());
        }
    }

    private System.Collections.IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    private System.Collections.IEnumerator FadeOutMusic()
    {
        float startVolume = musicSource.volume;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeDuration);
            yield return null;
        }

        musicSource.volume = 0f;
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;

        // при рестарте возвращаем музыку к сохранённому уровню
        if (musicSource != null)
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
            musicSource.volume = savedVolume;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
