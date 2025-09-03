using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainButton : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void OnTryAgain()
    {
        if (panel != null)
            panel.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
