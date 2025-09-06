using UnityEngine;

public class SettingController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject settingPanel;
    public GameObject gameUI;

    private bool isOpen = false;

    void Start()
    {
        if (settingPanel != null)
            settingPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        if (isOpen) return;

        isOpen = true;

        if (settingPanel != null)
            settingPanel.SetActive(true);

        if (gameUI != null)
            gameUI.SetActive(false);
    }

    public void CloseSettings()
    {
        if (!isOpen) return;

        if (settingPanel != null)
            settingPanel.SetActive(false);

        if (gameUI != null)
            gameUI.SetActive(true);

        isOpen = false;
    }
}
