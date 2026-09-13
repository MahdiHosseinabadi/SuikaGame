using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Text bestScoreText;

    void Start()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = bestScore.ToString();
    }

    public void SceneStart()
    {
        AudioManager.instance.PlaySound(SoundType.ButtonSound);
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        AudioManager.instance.PlaySound(SoundType.ButtonSound);
        Application.Quit();
        Debug.Log("Exit");
    }
}
