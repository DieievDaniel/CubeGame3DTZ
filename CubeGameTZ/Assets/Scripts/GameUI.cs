using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;

    [SerializeField] private ScoreData scoreData;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject[] objectsToDeactivate;

    public GameObject Panel
    {
        get { return panel; }
        set { panel = value; }
    }

    public GameObject[] ObjectsToDeactivate
    {
        get { return objectsToDeactivate; }
        set { objectsToDeactivate = value; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DisplayScore();
    }

    public void DisplayScore()
    {
        scoreText.text = scoreData.Score.ToString();
        gameOverScoreText.text = "Your Score is: " + scoreData.Score.ToString();
    }
}
