
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;

    public bool showFinalScore = false;

    private int score = 0;

    public static int FinalScore { get; private set; }

    void Start()
    {
        if (showFinalScore)
        {
            if (scoreText != null)
            {
                scoreText.text = "Final Score: " + FinalScore;
            }
        }
        else
        {
            score = 0;
            FinalScore = 0;
            UpdateScore();
        }
    }

    public void AddScore(int points)
    {
        score += points;

        FinalScore = score;

        UpdateScore();
    }

    void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
