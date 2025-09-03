using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections; // для корутин

public class QuizManager : MonoBehaviour
{
    [Header("Quiz Data")]
    public GameModel gameModel;
    public string levelName;
    public GameObject quizCanvas;

    [Header("UI References")]
    public TextMeshProUGUI questionText;
    [SerializeField] private Button[] answerButtons = new Button[4];

    [Header("Player Reference")]
    public PlayerController player;

    [Header("Speed Panel")]
    [SerializeField] private GameObject speedPanel;

    [Header("Feedback Panel")]
    [SerializeField] private GameObject feedbackPanel;  
    [SerializeField] private TextMeshProUGUI feedbackText; 
    [SerializeField] private float feedbackFadeDuration = 0.5f; 
    [SerializeField] private float feedbackShowTime = 1.5f; 

    private Level currentLevel;
    private QuestionData currentQuestion;
    private CanvasGroup feedbackCanvasGroup;

    void Start()
    {
        LoadLevel();
        ShowQuestion();

        if (feedbackPanel != null)
        {
            feedbackCanvasGroup = feedbackPanel.GetComponent<CanvasGroup>();
            if (feedbackCanvasGroup == null)
                feedbackCanvasGroup = feedbackPanel.AddComponent<CanvasGroup>();

            feedbackPanel.SetActive(false);
            feedbackCanvasGroup.alpha = 0f;
        }
    }

    void LoadLevel()
    {
        foreach (var lvl in gameModel.Levels)
        {
            if (lvl.LevelName == levelName)
            {
                currentLevel = lvl;
                break;
            }
        }

        if (currentLevel == null)
        {
            Debug.LogError("Level not found in GameModel: " + levelName);
        }
    }

    void ShowQuestion()
    {
        if (currentLevel == null || currentLevel.Questions.Length == 0)
        {
            Debug.LogError("No questions in this level!");
            return;
        }

        currentQuestion = currentLevel.Questions[0];

        if (questionText != null)
            questionText.text = currentQuestion.Question;

        string[] answers = {
            currentQuestion.Answer1,
            currentQuestion.Answer2,
            currentQuestion.Answer3,
            currentQuestion.Answer4
        };

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (answerButtons[i] == null) continue;

            TextMeshProUGUI btnText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (btnText != null)
                btnText.text = answers[i];

            int index = i;
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
        }
    }

    void OnAnswerSelected(int index)
    {
        if (index == currentQuestion.CorrectAnswerIndex)
        {
            Debug.Log("Правильно!");
            if (player != null)
                player.ChangeSpeed(+4);

            if (feedbackText != null)
                feedbackText.text = "Correct\nSpeeding Up!!!";
        }
        else
        {
            Debug.Log("Неправильно!");
            if (player != null)
                player.ChangeSpeed(-4);

            if (feedbackText != null)
                feedbackText.text = "Incorrect\nSlowing Down!";
        }

        if (speedPanel != null)
            speedPanel.SetActive(true);

        if (quizCanvas != null)
            quizCanvas.SetActive(false);

        if (feedbackPanel != null)
            StartCoroutine(ShowFeedbackAndResume());
    }

    private IEnumerator ShowFeedbackAndResume()
    {
        Time.timeScale = 0f;
        feedbackPanel.SetActive(true);

        float t = 0f;
        while (t < feedbackFadeDuration)
        {
            t += Time.unscaledDeltaTime;
            feedbackCanvasGroup.alpha = Mathf.Clamp01(t / feedbackFadeDuration);
            yield return null;
        }
        feedbackCanvasGroup.alpha = 1f;

        yield return new WaitForSecondsRealtime(feedbackShowTime);

        t = 0f;
        while (t < feedbackFadeDuration)
        {
            t += Time.unscaledDeltaTime;
            feedbackCanvasGroup.alpha = 1f - Mathf.Clamp01(t / feedbackFadeDuration);
            yield return null;
        }
        feedbackCanvasGroup.alpha = 0f;
        feedbackPanel.SetActive(false);

        Time.timeScale = 1f;
    }
}
