using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pooler to control projectiles so that we won't be creating and destroying all the time
/// </summary>
public class ProjectilePooler : MonoBehaviour
{
    public static ProjectilePooler Instance;
    /// <summary>
    /// References to the projectile prefab
    /// </summary>
    public GameObject projectilePrefab;

    /// <summary>
    /// Size of the pool. This is the default value and will be updated via Constants.json
    /// </summary>
    private int poolSize = 10;

    /// <summary>
    /// List that contains the list of projectiles to be used
    /// </summary>
    private Queue<GameObject> projectilePool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    private void Start()
    {
        poolSize = ConstantsLoader.Instance.projectilePoolSize;
        InitializePool();
    }

    /// <summary>
    /// Initializes the projectile pool once per game load
    /// </summary>
    private void InitializePool()
    {
        projectilePool.Clear();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform);
            projectile.SetActive(false);
            projectilePool.Enqueue(projectile);
        }
    }

    /// <summary>
    /// Retrieves the first available gameobject from the queue
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Returns the used projectile back to the pool
    /// </summary>
    /// <param name="projectile"></param>
    public void ReturnProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
        projectilePool.Enqueue(projectile);
    }

    /// <summary>
    /// Freezes all active projectiles in place
    /// </summary>
    public void FreezeAllProjectiles()
    {
        foreach(Transform projectile in transform)
        {
           if (projectile.gameObject.activeSelf)
            {
                projectile.GetComponent<ProjectileStateMachine>().FreezeProjectile();
            }
        }
    }

    /// <summary>
    /// Returns all active projectiles back to the queue
    /// </summary>
    public void ResetPool()
    {
        foreach(Transform projectile in transform)
        {
            if (projectile.gameObject.activeSelf)
            {
                ReturnProjectile(projectile.gameObject);
            }
        }
    }
}
