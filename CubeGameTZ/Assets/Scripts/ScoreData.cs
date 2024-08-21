using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData : MonoBehaviour
{
    public static ScoreData instance;

    private int score;

    private void Awake()
    {
        instance = this;
        LoadScore(); 
    }

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            SaveScore(); 
        }
    }

    public void AddScore(int value)
    {
        score += value;
        SaveScore(); 
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save(); 
    }

    private void LoadScore()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }
        else
        {
            score = 0; 
        }
    }
    public void ResetScore()
    {
        score = 0;
        SaveScore(); 
    }
}
