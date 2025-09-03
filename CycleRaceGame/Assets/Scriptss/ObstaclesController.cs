using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private float fadeDuration = 1f;

    [Header("UI References")]
    [SerializeField] private GameObject speedPanel;

    private CanvasGroup canvasGroup;
    private bool isGameOver = false;

    private void Start()
    {
        Time.timeScale = 1f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
            canvasGroup = gameOverPanel.GetComponent<CanvasGroup>();

            if (canvasGroup == null)
            {
                canvasGroup = gameOverPanel.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isGameOver && gameOverPanel != null)
        {
            isGameOver = true;

            if (speedPanel != null)
                speedPanel.SetActive(false);

            Time.timeScale = 0f;

            gameOverPanel.SetActive(true);
            StartCoroutine(FadeIn());
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

    public void RestartScene()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
