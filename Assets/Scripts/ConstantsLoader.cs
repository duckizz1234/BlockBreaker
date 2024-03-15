using UnityEngine;
using System.IO;

public class ConstantsLoader : MonoBehaviour
{
    public static ConstantsLoader Instance;

    public int maxMessages;
    public int minNumOfBlocks;
    public int maxNumOfBlocks;
    public int turretRotationSpeed;
    public int minBlockHealth;
    public int maxBlockHealth;
    public int projectileSpeed;
    public int projectileLifeSpan;
    public int projectilePoolSize;
    public int minDistanceBetweenBlocks;
    public bool showJuice;
    
    public const string WallTag = "Wall";
    public const string BlockTag = "Block";
    public const string ProjectileTag = "Projectile";
    public const string PlayerTag = "Player";
    public const string BlockBounceBool = "IsHit";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadConstants();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Load the constants from json file
    /// </summary>
    void LoadConstants()
    {
        string path = Application.streamingAssetsPath + "/Constants.json";

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            ConstantsData constantsData = JsonUtility.FromJson<ConstantsData>(jsonData);
            maxMessages = constantsData.maxMessages;
            minNumOfBlocks = constantsData.minNumOfBlocks;
            maxNumOfBlocks = constantsData.maxNumOfBlocks;
            turretRotationSpeed = constantsData.turretRotationSpeed;
            minBlockHealth = constantsData.minBlockHealth;
            maxBlockHealth = constantsData.maxBlockHealth;
            projectileSpeed = constantsData.projectileSpeed;
            projectileLifeSpan = constantsData.projectileLifeSpan;
            projectilePoolSize = constantsData.projectilePoolSize;
            minDistanceBetweenBlocks = constantsData.minDistanceBetweenBlocks;
            showJuice = constantsData.showJuice;
        }
        else
        {
            Debug.LogError("Constants file not found!");
        }
    }
}

[System.Serializable]
public class ConstantsData
{
    public int maxMessages;
    public int minNumOfBlocks;
    public int maxNumOfBlocks;
    public int turretRotationSpeed;
    public int minBlockHealth;
    public int maxBlockHealth;
    public int projectileSpeed;
    public int projectileLifeSpan;
    public int projectilePoolSize;
    public int minDistanceBetweenBlocks;
    public bool showJuice;
}
