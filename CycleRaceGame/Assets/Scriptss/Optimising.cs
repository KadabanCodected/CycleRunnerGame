using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    [SerializeField] private float disableTime = 5f;

    private void OnEnable()
    {
        Invoke(nameof(DisableObject), disableTime);
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
