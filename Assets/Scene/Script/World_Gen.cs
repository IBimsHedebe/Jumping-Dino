using UnityEngine;

public class World_Gen : MonoBehaviour
{
    [Header("Platform Settings")]
    public float minLength = 3f;
    public float maxLength = 6f;
    public float minHeight = 3f;
    public float maxHeight = 10f;
    
    [Header("Generation Settings")]
    public int platformCount = 300;
    public float minGap = 10f;
    public float maxGap = 18f;
    
    [Header("Platform Prefabs")]
    public GameObject platformLeft, platformMiddleA, platformMiddleB, platformRight;
    
    [Header("Position")]
    public float posX = 0f;
    public float posY = 0f;

    void Start()
    {
        GenerateStartPlatform();
        GenerateJumpAndRunSections();
    }

    void GenerateStartPlatform()
    {
        int length = Mathf.RoundToInt(Random.Range(minLength, maxLength));
        GeneratePlatformSection(length, 0, "Start");
    }

    void GenerateJumpAndRunSections()
    {
        for (int i = 0; i < platformCount; i++)
        {
            // Add gap
            posX += Mathf.RoundToInt(Random.Range(minGap, maxGap));
            
            // Generate platform
            int length = Mathf.RoundToInt(Random.Range(minLength, maxLength));
            int height = Mathf.RoundToInt(Random.Range(minHeight, maxHeight));
            GeneratePlatformSection(length, height, $"Section_{i}");
        }
    }

    void GeneratePlatformSection(int length, int height, string sectionName)
    {
        for (int i = 0; i < length; i++)
        {
            var (platformPrefab, platformType) = GetPlatformPrefab(i, length);
            
            Vector3 position = new Vector3(posX + i, posY + height, 0);
            GameObject platform = Instantiate(platformPrefab, position, Quaternion.identity);
            platform.name = $"Platform_{platformType}_{sectionName}_{i}";
            platform.transform.parent = transform;
        }
        posX += length;
    }

    (GameObject prefab, string type) GetPlatformPrefab(int index, int totalLength)
    {
        return totalLength switch
        {
            1 => (platformMiddleA, "MiddleA"),
            2 => index == 0 ? (platformLeft, "Left") : (platformRight, "Right"),
            _ => index switch
            {
                0 => (platformLeft, "Left"),
                var i when i == totalLength - 1 => (platformRight, "Right"),
                _ => ((index - 1) % 2 == 0) ? (platformMiddleA, "MiddleA") : (platformMiddleB, "MiddleB")
            }
        };
    }
}