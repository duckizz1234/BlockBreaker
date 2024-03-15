using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform blockParent;

    void Start()
    {
        StartLevel();
    }

    void StartLevel()
    {
        SpawnBlocks();
    }

    void SpawnBlocks()
    {
        int numBlocks = Random.Range(ConstantsLoader.Instance.minNumOfBlocks, ConstantsLoader.Instance.maxNumOfBlocks);
        for (int i = 0; i < numBlocks; i++)
        {
            Vector2 spawnPos = GetValidSpawnPosition();
            Instantiate(blockPrefab, spawnPos, Quaternion.identity, blockParent);
        }
    }

    private Vector2 GetValidSpawnPosition()
    {
        Vector2 spawnPos;
        bool isValid = false;
        do
        {
            spawnPos = new Vector2(Random.Range(-5f, 5f), Random.Range(-3f, 3f));
            isValid = IsSpawnPositionValid(spawnPos);
        } while (!isValid);

        return spawnPos;
    }

    private bool IsSpawnPositionValid(Vector2 spawnPos)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPos, ConstantsLoader.Instance.minDistanceBetweenBlocks);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(ConstantsLoader.WallTag) || collider.CompareTag(ConstantsLoader.BlockTag))
            {
                return false;
            }
        }
        return true;
    }
}
