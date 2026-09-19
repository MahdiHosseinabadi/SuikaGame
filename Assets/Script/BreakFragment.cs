using UnityEngine;

public class BreakFragment : MonoBehaviour
{
    public float minRotationSpeed = -90f;
    public float maxRotationSpeed = 90f;

    float lifeTime = 2f;

    Vector3 startScale;
    Vector3 endScale = Vector3.zero;
    Vector2 randomScaleRange = new Vector2(0.4f, 1.2f);

    Rigidbody2D rigidBody;

    float currentScaleMultiplier;
    float timer;
    float rotationSpeed;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startScale = transform.localScale;
    }

    public void Spawn(Vector3 position)
    {
        transform.position = position;
        currentScaleMultiplier = Random.Range(randomScaleRange.x, randomScaleRange.y);
        transform.localScale = startScale * currentScaleMultiplier;

        timer = lifeTime;

        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        rigidBody.angularVelocity = rotationSpeed;
    }

    void Update()
    {
        if (timer <= 0) return;

        timer -= Time.unscaledDeltaTime;
        float time = Mathf.Clamp01(timer / lifeTime);
        transform.localScale = Vector3.Lerp(endScale, startScale * currentScaleMultiplier, time);

        if (timer <= 0)
        {
            rigidBody.linearVelocity = Vector2.zero;
            rigidBody.angularVelocity = 0f;
            Destroy(gameObject);
        }
    }
}
