using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PauseController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pausePanel;         // Панель паузы
    public GameObject gameUI;             // Основной UI игры (HUD)
    public TextMeshProUGUI countdownText; // Текст для 3-2-1

    [Header("Animation Settings")]
    public float baseScale = 1f;             // начальный размер текста
    public float countdownScale = 2f;        // во сколько раз увеличивать текст
    public float countdownDuration = 0.5f;   // длительность анимации одного числа

    private bool isPaused = false;
    public static bool IsGamePaused = false; // 🚩 глобальный флаг

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (countdownText != null)
            countdownText.gameObject.SetActive(false);

        IsGamePaused = false;
    }

    // 📌 Кнопка "Pause"
    public void PauseGame()
    {
        if (isPaused) return;

        isPaused = true;
        IsGamePaused = true; // 🚩 блокируем управление

        Time.timeScale = 0f; // стопаем игру

        if (pausePanel != null)
            pausePanel.SetActive(true);

        if (gameUI != null)
            gameUI.SetActive(false); // скрываем HUD
    }

    // 📌 Кнопка "Close"
    public void ResumeGame()
    {
        if (!isPaused) return;

        if (pausePanel != null)
            pausePanel.SetActive(false);

        StartCoroutine(CountdownAndResume());
    }

    private IEnumerator CountdownAndResume()
    {
        if (countdownText == null)
        {
            Time.timeScale = 1f;
            isPaused = false;
            IsGamePaused = false; // 🚩 разблокируем управление
            if (gameUI != null) gameUI.SetActive(true);
            yield break;
        }

        countdownText.gameObject.SetActive(true);

        // Цикл отсчета 3-2-1
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            countdownText.transform.localScale = Vector3.one * baseScale;

            float t = 0f;
            while (t < countdownDuration)
            {
                t += Time.unscaledDeltaTime;
                float scale = Mathf.Lerp(baseScale, countdownScale, t / countdownDuration);
                countdownText.transform.localScale = Vector3.one * scale;
                yield return null;
            }
        }

        countdownText.gameObject.SetActive(false);

        // Возвращаем HUD и игру
        if (gameUI != null)
            gameUI.SetActive(true);

        Time.timeScale = 1f;
        isPaused = false;
        IsGamePaused = false; // 🚩 теперь можно снова управлять
    }
}
