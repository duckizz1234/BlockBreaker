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
}
