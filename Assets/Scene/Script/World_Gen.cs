using UnityEngine;

public class World_Gen : MonoBehaviour
{
    public float minLength = 2f;
    public float maxLength = 6f;
    public float minHeight = 1f;
    public float maxHeight = 5f;

    int platformCount = 30;
    public float minGap = 5f;
    public float maxGap = 9f;
    public GameObject platform;

    public float posX = 0f;
    public float posY = 0f;

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
        for (int i = 0; i < platformCount; i++)
        {
            // Randomly generate the position and size of the platform

            GameObject obj = Instantiate(platform);
            obj.transform.position = new Vector2(posX + Random.Range(minLength, maxLength) / 2 + Random.Range(minGap, maxGap), posY + Random.Range(minHeight, maxHeight));
            obj.transform.localScale = new Vector2(Random.Range(minLength, maxLength), 1);
            obj.name = "Platform_" + (i + 1);

            posX = obj.transform.position.x + Random.Range(minGap, maxGap) + Random.Range(minLength, maxLength) / 2;
        }
        GenerateStaircase();
    }

    public void GenerateStaircase()
    {

        float scLength = Random.Range(minLength * 9, maxLength * 9);
        float scHeight = Random.Range(minHeight * 30, maxHeight * 30);

        for (int i = 0; i < scHeight; i++)
        {
            GameObject obj = Instantiate(platform);
            obj.transform.position = new Vector2(posX, posY + i);
            obj.transform.localScale = new Vector2(1, 1);
            obj.name = "Wall_" + i;

            GameObject obj2 = Instantiate(platform);
            obj2.transform.position = new Vector2(posX + scLength, posY + i);
            obj2.transform.localScale = new Vector2(1, 1);
            obj2.name = "Wall_" + i + "_2";
        }
        Destroy(GameObject.Find("Wall_1"));
        Destroy(GameObject.Find("Wall_2"));
        Destroy(GameObject.Find("Wall_3"));
        Destroy(GameObject.Find("Wall_4"));
        Destroy(GameObject.Find("Wall_5"));

        for (int i = 0; i < scHeight; i += 6)
        {
            float lenght = Random.Range(minLength, maxLength);
            GameObject obj = Instantiate(platform);
            obj.transform.position = new Vector2(posX + scLength - lenght, posY + i);
            obj.transform.localScale = new Vector2(lenght, 1);
            obj.name = "Stair_" + i;
        }
        for (int i = 0; i < scHeight; i += 6)
        {
            float lenght = Random.Range(minLength, maxLength);
            GameObject obj = Instantiate(platform);
            obj.transform.position = new Vector2(posX + lenght, posY + i);
            obj.transform.localScale = new Vector2(lenght, 1);
            obj.name = "Stair_" + i + "_2";
        }
    }
}