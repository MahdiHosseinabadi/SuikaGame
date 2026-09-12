using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;
    public TMP_Text gameOverScoreText;
    public TMP_Text bestScoreText;

    public static ScoreManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    public void ScoreGame()
    {
        gameOverScoreText.text = score.ToString();
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (score >= bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
        bestScoreText.text = bestScore.ToString();
    }
}
