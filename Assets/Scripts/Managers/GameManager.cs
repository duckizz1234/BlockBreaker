using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform blockParent;
    public Transform[] walls;
    public static GameManager Instance;
    private List<GameObject> listOfBlocks = new List<GameObject>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartLevel()
    {
        SpawnBlocks();
    }

    private void SpawnBlocks()
    {
        int numBlocks = Random.Range(ConstantsLoader.Instance.minNumOfBlocks, ConstantsLoader.Instance.maxNumOfBlocks);
        listOfBlocks.Clear();
        for (int i = 0; i < numBlocks; i++)
        {
            Vector2 spawnPos = GetValidSpawnPosition();
            listOfBlocks.Add(Instantiate(blockPrefab, spawnPos, Quaternion.identity, blockParent));
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

    public void RemoveBlockFromList(GameObject block)
    {
        listOfBlocks.Remove(block);
        CheckAnyBlocksLeft();
    }

    private void CheckAnyBlocksLeft()
    {
        if (listOfBlocks.Count == 0)
        {
            GetComponent<MenuStateMachine>().ChangeState(new ReplayState());
        }
    }
}
