using UnityEngine;

public class QuestionAnim : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 50f;

    [Header("Floating Settings")]
    [SerializeField] private float floatAmplitude = 0.25f; 
    [SerializeField] private float floatFrequency = 1f;  

    private Vector3 startPos;

    void Start()
    {

        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
