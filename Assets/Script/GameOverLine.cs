using System.Collections;
using UnityEngine;

public class GameOverLine : MonoBehaviour
{
    bool gameOverStarted = false;
    Coroutine gameOverCoroutine;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Fruit fruit = collision.GetComponent<Fruit>();

        if (!fruit) return;

        if (!gameOverStarted)
        {
            gameOverStarted = true;
            gameOverCoroutine = StartCoroutine(GameOverTime());
        }
    }

    IEnumerator GameOverTime()
    {
        yield return new WaitForSeconds(2f);

        GameManager.instance.GameOver();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Fruit fruit = collision.GetComponent<Fruit>();

        if (!fruit) return;

        if (gameOverCoroutine != null)
        {
            StopCoroutine(gameOverCoroutine);
            gameOverCoroutine = null;
            gameOverStarted = false;
        }
    }
}
