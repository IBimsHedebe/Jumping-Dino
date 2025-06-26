using UnityEngine;

public class World_Gen : MonoBehaviour
{
    public float minLength = 2f;
    public float maxLength = 6f;
    public float minHeight = 1f;
    public float maxHeight = 5f;

    int platformCount = 20;
    public float minGap = 5f;
    public float maxGap = 9f;
    public GameObject platform;

    void Start()
    {
        GenerateSpawnPlatform();
        GeneratePlatforms(); 
    }

    public void GenerateSpawnPlatform()
    {
        GameObject obj = Instantiate(platform);
        obj.transform.position = new Vector2(0, 0);
        obj.transform.localScale = new Vector2(Random.Range(minLength, maxLength), 1);
        obj.name = "Platform_0";
    }

    public void GeneratePlatforms()
    {
        float posX = 0f;
        float posY = 0f;

        for (int i = 0; i < platformCount; i++)
        {
            // Randomly generate the position and size of the platform
            float length = Random.Range(minLength, maxLength);
            float height = Random.Range(minHeight, maxHeight);
            float gap = Random.Range(minGap, maxGap);

            GameObject obj = Instantiate(platform);
            obj.transform.position = new Vector2(posX + length / 2 + gap, posY + height);
            obj.transform.localScale = new Vector2(length, 1);
            obj.name = "Platform_" + (i + 1);

            posX = obj.transform.position.x + gap + length / 2;
        }
    }
}