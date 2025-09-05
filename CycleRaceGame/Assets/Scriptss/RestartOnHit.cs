using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOnHit : MonoBehaviour
{
    // ���� ����������� ��������
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("interactive")) // ��� "itteractive", ���� ��� ������� ���
            RestartLevel();
    }

    // �� ������, ���� ������ ������������ ��-���������� ��������
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