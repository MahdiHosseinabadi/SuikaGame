using UnityEngine;

public class PopAnimationFruit : MonoBehaviour
{
    public float duration = 0.15f;
    public float targetScale = 1.2f;

    Vector3 originalScale;
    float timer = 0f;

    void Start()
    {
        originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float time = timer / duration;

        if (time < 0.5f)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, originalScale * targetScale, time * 2);
        }
        else
        {
            transform.localScale = Vector3.Lerp(originalScale * targetScale, originalScale, (time - 0.5f) * 2);
        }

        if (time >= 1f)
        {
            transform.localScale = originalScale;
            Destroy(this);
        }
    }
}
