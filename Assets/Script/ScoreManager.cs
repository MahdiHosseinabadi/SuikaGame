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
        instance = this;
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
            PlayerPrefs.Save();

            AudioManager.instance.PlaySound(SoundType.NewRecordSound);
        }

        bestScoreText.text = bestScore.ToString();
    }

    public void ResetBestScore()
    {
        PlayerPrefs.SetInt("BestScore", 0);
        PlayerPrefs.Save();

        bestScoreText.text = "0";
    }
}