using UnityEngine;

[CreateAssetMenu(fileName = "GameModel", menuName = "Quiz/Game Model")]
public class GameModel : ScriptableObject
{
    public Level[] Levels;
}

[System.Serializable]
public class Level
{
    public string LevelName;
    public QuestionData[] Questions;
}

[System.Serializable]
public class QuestionData
{
    [TextArea] public string Question;
    public string Answer1;
    public string Answer2;
    public string Answer3;
    public string Answer4;
    [Range(0, 3)] public int CorrectAnswerIndex;
}
