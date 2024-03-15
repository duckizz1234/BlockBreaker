using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform blockParent;

    void Start()
    {
        SpawnBlocks();
    }

    void SpawnBlocks()
    {
        int numBlocks = Random.Range(ConstantsLoader.Instance.minNumOfBlocks, ConstantsLoader.Instance.maxNumOfBlocks);
        for (int i = 0; i < numBlocks; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-5f, 5f), Random.Range(-3f, 3f));
            Instantiate(blockPrefab, spawnPos, Quaternion.identity, blockParent);
        }
    }
}
