using UnityEngine;

public class QuizTrigger : MonoBehaviour
{
    [Header("UI")]
    public GameObject quizCanvas;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return; 

        if (other.CompareTag("Player")) 
        {
            if (quizCanvas != null)
            {
                quizCanvas.SetActive(true); 
                Time.timeScale = 0f; 
                Debug.Log("Открылся Canvas викторины! Игра на паузе.");
            }

            triggered = true;
            GetComponent<Collider>().enabled = false;
        }
    }
}
