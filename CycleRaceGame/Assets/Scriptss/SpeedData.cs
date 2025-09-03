using UnityEngine;
using TMPro; 

public class SpeedDisplay : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TextMeshProUGUI speedText; 

    private void Update()
    {
        if (player != null && speedText != null)
        {
            speedText.text = player.GetSpeed().ToString("0") + " km/h";
        }
    }
}
