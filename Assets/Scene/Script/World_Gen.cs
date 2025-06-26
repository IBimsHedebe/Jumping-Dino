using UnityEngine;

/// <summary>
/// Generates a procedural 2D platformer world with platforms extending in both directions
/// and vertical staircases for climbing challenges
/// </summary>
public class World_Gen : MonoBehaviour
{
    [Header("Platform Size Settings")]
    public float minLength = 2f;    // Minimum platform width
    public float maxLength = 6f;    // Maximum platform width
    public float minHeight = 1f;    // Minimum height variation between platforms
    public float maxHeight = 5f;    // Maximum height variation between platforms

    [Header("Generation Settings")]
    int platformCount = 30;         // Number of platforms to generate in each direction
    public float minGap = 5f;       // Minimum gap between platforms
    public float maxGap = 9f;       // Maximum gap between platforms
    public GameObject platform;    // Platform prefab to instantiate

    [Header("Position Tracking")]
    public float posX = 0f;         // Current X position for platform generation
    public float posY = 0f;         // Current Y position for platform generation
    public float scHeight = 0f;     // Height of the staircase (shared between methods)

    /// <summary>
    /// Initialize world generation sequence
    /// </summary>
    void Start()
    {
        GenerateSpawnPlatform();    // Create starting platform at origin
        GeneratePlatformsRight();   // Generate platforms extending to the right
        posY = scHeight;            // Set Y position to staircase height for left generation
        GeneratePlatformsLeft();    // Generate platforms extending to the left
    }

    /// <summary>
    /// Creates the initial spawn platform at the world origin (0,0)
    /// This serves as the player's starting point
    /// </summary>
    public void GenerateSpawnPlatform()
    {
        GameObject obj = Instantiate(platform);                                         // Create platform instance
        obj.transform.position = new Vector2(0, 0);                                   // Position at world origin
        obj.transform.localScale = new Vector2(Random.Range(minLength, maxLength), 1); // Random width, fixed height
        obj.name = "Platform_0";                                                       // Name for easy identification
    }

    /// <summary>
    /// Generates a series of platforms extending to the left from the spawn point
    /// Called after right-side generation to use the staircase height
    /// </summary>
    public void GeneratePlatformsLeft()
    {
        for (int i = 0; i < platformCount; i++)
        {
            // Create new platform instance
            GameObject obj = Instantiate(platform);
            
            // Position platform to the left with random gap and height variation
            float xPosition = posX - Random.Range(minLength, maxLength) / 2 - Random.Range(minGap, maxGap);
            float yPosition = posY + Random.Range(minHeight, maxHeight);
            obj.transform.position = new Vector2(xPosition, yPosition);
            
            // Set random platform width
            obj.transform.localScale = new Vector2(Random.Range(minLength, maxLength), 1);
            obj.name = "-Platform_" + (i + 1);  // Negative prefix to indicate left side
            
            // Update position tracker for next platform
            posX = obj.transform.position.x - Random.Range(minGap, maxGap) - Random.Range(minLength, maxLength) / 2;
        }
        GenerateStaircase(false);  // Create left-side staircase at the end
    }
    /// <summary>
    /// Generates a series of platforms extending to the right from the spawn point
    /// Called first to establish the staircase height for left-side generation
    /// </summary>
    public void GeneratePlatformsRight()
    {
        for (int i = 0; i < platformCount; i++)
        {
            // Create new platform instance
            GameObject obj = Instantiate(platform);
            
            // Position platform to the right with random gap and height variation
            float xPosition = posX + Random.Range(minLength, maxLength) / 2 + Random.Range(minGap, maxGap);
            float yPosition = posY + Random.Range(minHeight, maxHeight);
            obj.transform.position = new Vector2(xPosition, yPosition);
            
            // Set random platform width
            obj.transform.localScale = new Vector2(Random.Range(minLength, maxLength), 1);
            obj.name = "Platform_" + (i + 1);  // Numbered platform for identification
            
            // Update position tracker for next platform
            posX = obj.transform.position.x + Random.Range(minGap, maxGap) + Random.Range(minLength, maxLength) / 2;
        }
        GenerateStaircase(true);  // Create right-side staircase at the end
    }

    /// <summary>
    /// Creates a vertical staircase structure with alternating platforms for climbing
    /// </summary>
    /// <param name="isRight">True for right-side staircase, false for left-side</param>
    public void GenerateStaircase(bool isRight)
    {
        // Define staircase dimensions
        float scLength = Random.Range(10, 15);  // Width of the staircase chamber
        scHeight = Random.Range(30, 50);        // Height of the staircase (stored for left-side generation)

        // Store wall references for entrance creation
        GameObject[] leftWalls = new GameObject[(int)scHeight];
        GameObject[] rightWalls = new GameObject[(int)scHeight];

        // Create vertical walls on both sides of the staircase
        for (int i = 0; i < scHeight; i++)
        {
            // Left wall
            GameObject obj = Instantiate(platform);
            obj.transform.position = new Vector2(posX, posY + i);
            obj.transform.localScale = new Vector2(1, 1);  // Single unit blocks
            obj.name = "Wall_" + i + (isRight ? "_Right" : "_Left");
            leftWalls[i] = obj;  // Store reference

            // Right wall
            GameObject obj2 = Instantiate(platform);
            if (isRight)
            {
                obj2.transform.position = new Vector2(posX + scLength, posY + i);
            }
            else
            {
                obj2.transform.position = new Vector2(posX - scLength, posY + i);
            }
            obj2.transform.localScale = new Vector2(1, 1);  // Single unit blocks
            obj2.name = "Wall_" + i + "_2" + (isRight ? "_Right" : "_Left");
            rightWalls[i] = obj2;  // Store reference
        }

        // Create entrance gap by destroying some wall blocks using their names
        if (isRight)
        {
            // For right-side staircase, remove left wall blocks for entrance (player enters from left)
            for (int i = 4; i < 9; i++)
            {
                GameObject wallToDestroy = GameObject.Find("Wall_" + i + "_Right");
                if (wallToDestroy != null)
                {
                    Destroy(wallToDestroy);  // Remove left wall to create entrance from left
                }
            }
        }
        else
        {
            // For left-side staircase, remove right wall blocks for entrance (player enters from right)
            for (int i = 4; i < 9; i++)
            {
                GameObject wallToDestroy = GameObject.Find("Wall_" + i + "_Left");
                if (wallToDestroy != null)
                {
                    Destroy(wallToDestroy);  // Remove right wall to create entrance from right
                }
            }
        }

        // Create alternating platforms for climbing (every 6 units vertically)
        for (int i = 0; i < scHeight; i += 6)
        {
            float length = Random.Range(minLength, maxLength);  // Random platform width
            GameObject obj = Instantiate(platform);
            
            // Position platforms based on staircase side (same for both - may need adjustment)
            if (isRight)
            {
                obj.transform.position = new Vector2(posX + scLength - (length / 2), posY + i);
            }
            else
            {
                obj.transform.position = new Vector2(posX - scLength + (length / 2), posY + i);
            }
            
            obj.transform.localScale = new Vector2(length, 1);
            obj.name = "Stair_" + i + (isRight ? "_Right" : "_Left");  // Descriptive naming
        }

        // Create offset platforms (every 6 units, starting at height 3) for zigzag climbing
        for (int i = 3; i < scHeight; i += 6)
        {
            float length = Random.Range(minLength, maxLength);  // Random platform width
            GameObject obj = Instantiate(platform);
            
            // Position offset platforms (same for both - may need adjustment)
            if (isRight)
            {
                obj.transform.position = new Vector2(posX + (length / 2), posY + i);
            }
            else
            {
                obj.transform.position = new Vector2(posX - (length / 2), posY + i);
            }
            
            obj.transform.localScale = new Vector2(length, 1);
            obj.name = "Stair_" + i + "_Alt" + (isRight ? "_Right" : "_Left");  // Alternative step naming
        }
    }
}