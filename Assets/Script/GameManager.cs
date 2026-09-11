using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    public Transform spawnPoint;
    public float leftLimit = -2f;
    public float rightLimit = 2f;

    GameObject currentFruit;

    void Start()
    {
        SpawnFruit();
    }

    void SpawnFruit()
    {
        int randomFruit = Random.Range(0, fruitPrefabs.Length);
        currentFruit = Instantiate(fruitPrefabs[randomFruit], spawnPoint.position, Quaternion.identity);
        currentFruit.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    void Update()
    {
        if (!currentFruit) return;

        Vector2 screenPosition;

        bool touch = Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed;
        bool touchReleased = Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasReleasedThisFrame;

        bool mouse = Mouse.current != null && Mouse.current.leftButton.isPressed;
        bool mouseReleased = Mouse.current != null && Mouse.current.leftButton.wasReleasedThisFrame;


        if (touch)
        {
            screenPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else if (mouse)
        {
            screenPosition = Mouse.current.position.ReadValue();
        }
        else if (!touchReleased && !mouseReleased)
        {
            return;
        }
        else
        {
            screenPosition = Vector2.zero;
        }

        if (touch || mouse)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 0));
            float x = Mathf.Clamp(worldPosition.x, leftLimit, rightLimit);
            currentFruit.transform.position = new Vector3(x, spawnPoint.position.y, 0);
        }

        if (touchReleased || mouseReleased)
        {
            Rigidbody2D rigidBody = currentFruit.GetComponent<Rigidbody2D>();
            rigidBody.gravityScale = 1;

            currentFruit = null;
            Invoke(nameof(SpawnFruit), 0.7f);
        }
    }
}
