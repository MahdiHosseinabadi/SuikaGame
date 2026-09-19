using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int fruitLevel;
    public GameObject fragmentPrefab;
    public int fragmentCount;

    bool hasMerged = false;
    bool canMerged = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();

        if (!otherFruit) return;

        if (!canMerged || !otherFruit.canMerged) return;

        if (hasMerged || otherFruit.hasMerged) return;

        if (otherFruit.fruitLevel == fruitLevel)
        {
            hasMerged = true;
            otherFruit.hasMerged = true;

            Vector3 mergePosition = (transform.position + otherFruit.transform.position) / 2;

            if (GameManager.instance.LastFruit(fruitLevel))
            {
                for (int i = 0; i < fragmentCount; i++)
                {
                    Vector3 offset = new Vector3(Random.Range(-2f * transform.localScale.x, 2f * transform.localScale.x), Random.Range(-1f * transform.localScale.y, 1f * transform.localScale.y), 0f);
                    GameObject fragment = Instantiate(fragmentPrefab, transform.position + offset, Quaternion.identity);
                    fragment.GetComponent<BreakFragment>().Spawn(transform.position + offset);
                }
                AudioManager.instance.PlaySound(SoundType.Break);
            }

            GameManager.instance.MergeFruit(fruitLevel + 1, mergePosition);
            ScoreManager.instance.AddScore((int)Mathf.Pow(2, fruitLevel) * 10);
            ScoreManager.instance.TotalMarge();

            Destroy(otherFruit.gameObject);
            Destroy(gameObject);
        }
    }

    public void EnableMerge()
    {
        canMerged = true;
    }
}