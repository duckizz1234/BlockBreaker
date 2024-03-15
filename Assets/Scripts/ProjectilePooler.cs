using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
    public static ProjectilePooler Instance;

    public GameObject projectilePrefab;
    public int poolSize = 10;
    private Queue<GameObject> projectilePool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform);
            projectile.SetActive(false);
            projectilePool.Enqueue(projectile);
        }
    }

    public GameObject GetProjectile()
    {
        if (projectilePool.Count == 0)
        {
            Debug.LogWarning("Projectile pool is empty! Consider increasing the pool size.");
            return null;
        }

        GameObject projectile = projectilePool.Dequeue();
        return projectile;
    }

    public void ReturnProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
        projectilePool.Enqueue(projectile);
    }
}
