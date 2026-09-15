using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject gameArea;

    public static UIManager instance;

    void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        AudioManager.instance.PlaySound(SoundType.GameOverSound);
        ScoreManager.instance.ScoreGame();

        gameArea.SetActive(false);
        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        AudioManager.instance.PlaySound(SoundType.ButtonSound);
        Time.timeScale = 1f;
        AudioManager.instance.musicSource.Stop();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        AudioManager.instance.PlaySound(SoundType.ButtonSound);
        Time.timeScale = 1f;
        AudioManager.instance.musicSource.Stop();
        SceneManager.LoadScene(0);
    }
}
