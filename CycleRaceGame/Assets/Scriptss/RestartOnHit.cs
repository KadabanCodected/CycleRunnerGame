using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOnHit : MonoBehaviour
{
    // Если используешь триггеры
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("interactive")) // или "itteractive", если так назовёшь тег
            RestartLevel();
    }

    // На случай, если будешь использовать не-триггерные коллизии
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("interactive"))
            RestartLevel();
    }

    private void RestartLevel()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}