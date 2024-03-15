using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform blockParent;
    public Transform[] walls;

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
        int maxAttempts = 100; // Limit the number of attempts to avoid infinite loop
        int attempts = 0;

        do
        {
            spawnPos = new Vector2(Random.Range(walls[0].position.x, walls[1].position.x),
                                   Random.Range(walls[2].position.y, walls[3].position.y));

            isValid = IsSpawnPositionValid(spawnPos);
            attempts++;
        } while (!isValid && attempts < maxAttempts);

        if (!isValid)
        {
            Debug.LogWarning("Failed to find a valid spawn position for the block.");
        }

        return spawnPos;
    }

    private bool IsSpawnPositionValid(Vector2 spawnPos)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPos, ConstantsLoader.Instance.minDistanceBetweenBlocks);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(ConstantsLoader.WallTag) || collider.CompareTag(ConstantsLoader.BlockTag) || collider.CompareTag(ConstantsLoader.PlayerTag))
            {
                return false;
            }
        }
        return true;
    }
}
