using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int fruitLevel;

    bool hasMerged = false;
    bool canMerged = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();

        if (!otherFruit) return;

        if (!canMerged || !otherFruit.canMerged) return;

        if (hasMerged || otherFruit.hasMerged) return;

        if (!GameManager.instance.LastFruit(fruitLevel))
        {
            if (otherFruit.fruitLevel == fruitLevel)
            {
                hasMerged = true;
                otherFruit.hasMerged = true;

                Vector3 mergePosition = (transform.position + otherFruit.transform.position) / 2;
                GameManager.instance.MergeFruit(fruitLevel + 1, mergePosition);
                Destroy(otherFruit.gameObject);
                Destroy(gameObject);
            }
        }
    }

    public void EnableMerge()
    {
        canMerged = true;
    }
}
