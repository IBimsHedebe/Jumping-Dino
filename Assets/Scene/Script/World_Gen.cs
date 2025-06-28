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
        int platformLength = Mathf.RoundToInt(Random.Range(minLength, maxLength));
        for (int i = 0; i < platformLength; i++) // Generate the Spawn Platform
        {
            GameObject plat = Instantiate(platform, new Vector3(posX + i, posY, 0), Quaternion.identity);
            plat.name = "Spawn_Platform_" + i;
            plat.transform.parent = transform; // Set parent to World_Gen for better organization
        }
        posX += platformLength; // Update position for the next platform
    }

    public void _JumpAndRunSections(bool isRight = true)
    {
        if (isRight)
        {
            // Generate a section to the right
            for (int i = 0; i < platformCount; i++)
            {
                // Add gap before placing platform
                int gap = Mathf.RoundToInt(Random.Range(minGap, maxGap));
                posX += gap;
                
                int platformLength = Mathf.RoundToInt(Random.Range(minLength, maxLength));
                int height = Mathf.RoundToInt(Random.Range(minHeight, maxHeight));

                // Create the platform section
                for (int j = 0; j < platformLength; j++)
                {
                    GameObject plat = Instantiate(platform, new Vector3(posX + j, posY + height, 0), Quaternion.identity);
                    plat.name = "Platform_Right_" + i + "_" + j;
                    plat.transform.parent = transform; // Set parent to World_Gen for better organization
                }
                posX += platformLength; // Update position for the next section
            }
        }
        else
        {
            // Generate a section to the left
        }
    }
}