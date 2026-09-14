using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Text bestScoreText;
    public GameObject recordPanel;
    public TMP_Text bestScoreTextPanel;
    public TMP_Text totalGameTextPanel;
    public TMP_Text totalMargeTextPanel;

    void Start()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = bestScore.ToString();
    }

    public void SceneStart()
    {
        AudioManager.instance.PlaySound(SoundType.ButtonSound);

        int totalGame = PlayerPrefs.GetInt("TotalGame", 0);
        totalGame++;
        PlayerPrefs.SetInt("TotalGame", totalGame);
        PlayerPrefs.Save();

        SceneManager.LoadScene(1);
    }

    public void OpenRecord()
    {
        AudioManager.instance.PlaySound(SoundType.ButtonSound);
        recordPanel.SetActive(true);

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreTextPanel.text = bestScore.ToString();

        int totalGame = PlayerPrefs.GetInt("TotalGame", 0);
        totalGameTextPanel.text = totalGame.ToString();

        int totalMarge = PlayerPrefs.GetInt("TotalMarge", 0);
        totalMargeTextPanel.text = totalMarge.ToString();
    }

    public void CloseRecord()
    {
        AudioManager.instance.PlaySound(SoundType.ButtonSound);
        recordPanel.SetActive(false);
    }

    public void ResetData()
    {
        AudioManager.instance.PlaySound(SoundType.ButtonSound);
        ScoreManager.instance.ResetAllData();

        bestScoreText.text = "0";
        bestScoreTextPanel.text = "0";
        totalGameTextPanel.text = "0";
        totalMargeTextPanel.text = "0";
    }

    public void Exit()
    {
        AudioManager.instance.PlaySound(SoundType.ButtonSound);
        Application.Quit();
        Debug.Log("Exit");
    }
}
