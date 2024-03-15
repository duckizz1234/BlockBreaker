using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Controls the flow of game
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Public References")]
    /// <summary>
    /// Prefab of block to be instantiated
    /// </summary>
    public GameObject blockPrefab;

    /// <summary>
    /// Parent of where the blocks will instantiate
    /// </summary>
    public Transform blockParent;

    /// <summary>
    /// List of walls for the game
    /// </summary>
    public Transform[] walls;

    /// <summary>
    /// References the turret that the player controls
    /// </summary>
    public TurretController player;

    public static GameManager Instance;
    
    /// <summary>
    /// master list of blocks generated for a level
    /// </summary>
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

    /// <summary>
    /// Starts a level
    /// </summary>
    public void StartLevel()
    {
        SpawnBlocks();
        ProjectilePooler.Instance.ResetPool();
        player.ToggleCanMove(true);
    }

    /// <summary>
    /// Spawn blocks in the play area
    /// </summary>
    private void SpawnBlocks()
    {
        ClearBlocks();

        // Randomly determines the number of blocks to spawn
        int numBlocks = Random.Range(ConstantsLoader.Instance.minNumOfBlocks, ConstantsLoader.Instance.maxNumOfBlocks);

        for (int i = 0; i < numBlocks; i++)
        {
            Vector2 spawnPos = GetValidSpawnPosition();
            listOfBlocks.Add(Instantiate(blockPrefab, spawnPos, Quaternion.identity, blockParent));
        }
    }

    /// <summary>
    /// Clears any existing blocks
    /// </summary>
    private void ClearBlocks()
    {
        // Destroys all blocks that may have remained behind
        foreach (var block in listOfBlocks)
        {
            Destroy(block);
        }
        listOfBlocks.Clear();
    }

    /// <summary>
    /// Gets a valid spawn position that is within the play area
    /// </summary>
    /// <returns>Vector 2 position for the new block</returns>
    private Vector2 GetValidSpawnPosition()
    {
        Vector2 spawnPos;
        bool isValid = false;
        int maxAttempts = 100; // Limit the number of attempts to avoid infinite loop
        int attempts = 0;

        // Tries to get valid spawn position but will time out if max attempts has been reached
        do
        {
            spawnPos = GetRandomPositionWithinBounds();
            isValid = IsSpawnPositionValid(spawnPos);
            attempts++;
        } while (!isValid && attempts < maxAttempts);

        if (!isValid)
        {
            Debug.LogWarning("Failed to find a valid spawn position for the block.");
        }

        return spawnPos;
    }

    /// <summary>
    /// Get random position within bounds of the walls of the game area
    /// </summary>
    /// <returns></returns>
    private Vector2 GetRandomPositionWithinBounds()
    {
        return new Vector2(Random.Range(walls[0].position.x, walls[1].position.x),
                           Random.Range(walls[2].position.y, walls[3].position.y));
    }

    /// <summary>
    /// Checks if the spawn position is valid by checking it is not colliding with other objects
    /// </summary>
    /// <param name="spawnPos">Position of the potential new block</param>
    /// <returns>True if no overlapping. False otherwise</returns>
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

    /// <summary>
    /// When a block has been destroyed, remove from the master tracking list
    /// </summary>
    /// <param name="block">Gameobject of the block that is going to be destroyed</param>
    public void RemoveBlockFromList(GameObject block)
    {
        listOfBlocks.Remove(block);
        CheckAnyBlocksLeft();
    }

    /// <summary>
    /// Checks if any blocks left and if none, triggers end level condition
    /// </summary>
    private void CheckAnyBlocksLeft()
    {
        if (listOfBlocks.Count == 0)
        {
            GetComponent<MenuStateMachine>().ChangeState(new ReplayState());
        }
    }
}
