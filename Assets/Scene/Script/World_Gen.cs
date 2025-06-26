using UnityEngine;

public class World_Gen : MonoBehaviour
{
    public float minLength = 2f;
    public float maxLength = 6f;
    public float minHeight = 1f;
    public float maxHeight = 5f;

    public int platformCount = 20;
    public float minGap = 5f;
    public float maxGap = 9f;
    public GameObject platform;

    void Start()
    {
        GeneratePlatforms();
    }

    public void GeneratePlatforms()
    {
        GameObject obj = Instantiate(platform);
        obj.transform.position = new Vector2(0, 0);
        obj.transform.localScale = new Vector2(Random.Range(minLength, maxLength), 1);
        obj.name = "Platform_0";
    }
}