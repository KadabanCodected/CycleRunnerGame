using UnityEngine;

public class QuizTrigger : MonoBehaviour
{
    [Header("UI")]
    public GameObject quizCanvas;   // Панель с квизом
    public GameObject speedPanel;   // Панель скорости (HUD)

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            if (quizCanvas != null)
            {
                quizCanvas.SetActive(true);
                Time.timeScale = 0f; // стопаем игру
            }

            // 📌 Скрываем панель скорости, пока открыт квиз
            if (speedPanel != null)
            {
                speedPanel.SetActive(false);
            }

            triggered = true;
            GetComponent<Collider>().enabled = false;
        }
    }
}
