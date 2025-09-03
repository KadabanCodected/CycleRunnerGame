using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject speedPanel; 
    [SerializeField] private float fadeDuration = 1f;

    private CanvasGroup canvasGroup;
    private bool isWin = false;

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
}
