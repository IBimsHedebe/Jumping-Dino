using UnityEngine;

public class World_Gen : MonoBehaviour
{
    public float minLength = 2f;
    public float maxLength = 6f;
    public float minHeight = 3f;
    public float maxHeight = 10f;

    int platformCount = 3000;
    public float minGap = 10f;
    public float maxGap = 18f;
    public GameObject platform, wall;

    public float posX = 0f;
    public float posY = 0f;
    public float scHeight = 0f;
    public float scWidth = 0f;

    void Start()
    {
        _StartPlatform();
        _JumpAndRunSections(true);
    }

    public void _StartPlatform()
    {
        float platformLength = Random.Range(minLength, maxLength);
        for (int i = 0; i < platformLength; i++) // Generate the Spawn Platform
        {
            GameObject plat = Instantiate(platform, new Vector3(posX + i, posY, 0), Quaternion.identity);
            plat.name = "Spawn_Platform_" + i;
        }
        posX += platformLength; // Update position for the next platform
    }

    public void _JumpAndRunSections(bool isRight = true)
    {
        if (isRight)
        {
            for (int i = 0; i < platformCount; i++)
            {
                float gap = Random.Range(minGap, maxGap);
                float height = Random.Range(minHeight, maxHeight);
                float platformLength = Random.Range(minLength, maxLength);

                for (int j = 0; j < platformLength; j++)
                {
                    GameObject plat = Instantiate(platform, new Vector3(posX + gap + j + platformLength / 2, posY + height, 0), Quaternion.identity);
                    plat.name = "Platform_Right_" + i + "_" + j;
                }
                posX += platformLength + gap; // Update position for the next platform
            }
        }
        else
        {
            for (int i = 0; i < platformCount; i++)
            {
                float gap = Random.Range(minGap, maxGap);
                float height = Random.Range(minHeight, maxHeight);
                float platformLength = Random.Range(minLength, maxLength);

                for (int j = 0; j < platformLength; j++)
                {
                    GameObject plat = Instantiate(platform, new Vector3(-posX - gap - j - platformLength / 2, posY + height, 0), Quaternion.identity);
                    plat.name = "Platform_Left_" + i + "_" + j;
                }
                posX -= platformLength - gap; // Update position for the next platform
            }
        }
    }
}