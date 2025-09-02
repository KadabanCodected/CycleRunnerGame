using UnityEngine;
using UnityEngine.UI;
using TMPro; // для TextMeshPro

public class QuizManager : MonoBehaviour
{
    [Header("Quiz Data")]
    public GameModel gameModel;
    public string levelName;       // Название уровня из GameModel
    public GameObject quizCanvas;  // Канвас, который будем скрывать

    [Header("UI References")]
    public TextMeshProUGUI questionText; // Текст вопроса
    [SerializeField] private Button[] answerButtons = new Button[4]; // 4 кнопки

    [Header("Player Reference")]
    public PlayerController player; // сюда перетащи объект игрока в инспекторе

    private Level currentLevel;
    private QuestionData currentQuestion;

    void Start()
    {
        LoadLevel();
        ShowQuestion();
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
        }
        else
        {
            Debug.Log("Неправильно!");
            if (player != null)
                player.ChangeSpeed(-4);
        }

        if (quizCanvas != null)
            quizCanvas.SetActive(false);

        Time.timeScale = 1f; // возобновляем игру
    }
}
