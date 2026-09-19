using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Text bestScoreText;

    public GameObject recordPanel;
    public TMP_Text bestScoreTextPanel;
    public TMP_Text totalGameTextPanel;
    public TMP_Text totalMargeTextPanel;
    public GameObject settingPanel;
    public Slider volumeSlider;
    public Toggle musicToggle;

    public static MainMenu instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = bestScore.ToString();

        float saveVolume = PlayerPrefs.GetFloat("Volume", 1f);
        AudioListener.volume = saveVolume;
        volumeSlider.value = saveVolume;

        AudioManager.instance.PlayMusic(AudioManager.instance.mainMenuMusic);
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

    public void OpenSetting()
    {
        AudioManager.instance.PlaySound(SoundType.ButtonSound);
        settingPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        AudioManager.instance.PlaySound(SoundType.ButtonSound);
        recordPanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();
    }

    public void MusicToggle()
    {
        AudioManager.instance.musicSource.mute = !musicToggle.isOn;
        PlayerPrefs.SetInt("MusicMute", musicToggle.isOn ? 0 : 1);
        PlayerPrefs.Save();
        int mute = PlayerPrefs.GetInt("MusicMute", 0);
        musicToggle.isOn = (mute == 0);
        AudioManager.instance.musicSource.mute = (mute == 1);
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
