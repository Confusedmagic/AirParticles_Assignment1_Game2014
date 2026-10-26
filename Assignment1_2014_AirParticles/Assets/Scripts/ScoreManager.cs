
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;

    private int score = 0;

    void Start()
    {
        UpdateScore();
    }

    public void AddScore(int points)
    {
        score += points;

        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
