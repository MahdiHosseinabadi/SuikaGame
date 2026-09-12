using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class GameManager : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    public Transform spawnPoint;
    public float leftLimit = -2.3f;
    public float rightLimit = 2.3f;
    public Image nextFruitImage;
    public Sprite[] fruitSprite;

    GameObject currentFruit;
    int currentFruitIndex;
    int nextFruitIndex;

    public static GameManager instance;

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

    void Start()
    {
        currentFruitIndex = Random.Range(0, fruitPrefabs.Length);
        nextFruitIndex = Random.Range(0, fruitPrefabs.Length);
        nextFruitImage.sprite = fruitSprite[nextFruitIndex];
        SpawnFruit();
    }

    void SpawnFruit()
    {
        currentFruit = Instantiate(fruitPrefabs[currentFruitIndex], spawnPoint.position, Quaternion.identity);
        currentFruit.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void MergeFruit(int nextLevel, Vector3 position)
    {
        if (nextLevel >= fruitPrefabs.Length) return;

        GameObject newFruit = Instantiate(fruitPrefabs[nextLevel], position, Quaternion.identity);
        newFruit.GetComponent<Fruit>().EnableMerge();
    }

    void Update()
    {
        if (!currentFruit) return;

        if (Touch.activeTouches.Count == 0) return;

        Touch touch = Touch.activeTouches[0];

        if (touch.isInProgress)
        {
            Vector2 touchPosition = touch.screenPosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, Camera.main.transform.position.z));

            float relocation = Mathf.Clamp(worldPosition.x, leftLimit, rightLimit);
            currentFruit.transform.position = new Vector3(relocation, spawnPoint.position.y, 0);
        }

        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            currentFruitIndex = nextFruitIndex;
            nextFruitIndex = Random.Range(0, fruitPrefabs.Length);
            nextFruitImage.sprite = fruitSprite[nextFruitIndex];

            Rigidbody2D rigidBody = currentFruit.GetComponent<Rigidbody2D>();
            rigidBody.gravityScale = 1;

            currentFruit.GetComponent<Fruit>().EnableMerge();

            currentFruit = null;
            Invoke(nameof(SpawnFruit), 0.7f);
        }
    }

    public bool LastFruit(int level)
    {
        return level >= fruitPrefabs.Length - 1;
    }

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
}