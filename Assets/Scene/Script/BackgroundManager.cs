using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [System.Serializable]
    public class BackgroundLayer
    {
        public string spriteName;     // Image name (without extension)
        public float scrollSpeed;     // Speed of scrolling
        [HideInInspector] public GameObject[] parts; // Four parts for looping
    }

    public BackgroundLayer[] layers;
    public Camera mainCamera;
    public Transform player; // Reference to the player transform
    
    private Vector3 lastPlayerPosition;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        // Find player if not assigned
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
                Debug.Log("BackgroundManager: Found player automatically - " + playerObj.name);
            }
            else
            {
                Debug.LogWarning("BackgroundManager: No player found! Make sure your player GameObject has the 'Player' tag or assign the player manually in the inspector.");
            }
        }

        // Initialize last player position
        if (player != null)
            lastPlayerPosition = player.position;

        foreach (var layer in layers)
        {
            // Load sprite from Resources
            Sprite sprite = Resources.Load<Sprite>("comcept Worlds/Green lands/Background/" + layer.spriteName);
            if (sprite == null)
            {
                Debug.LogError("Sprite not found: " + layer.spriteName);
                continue;
            }

            layer.parts = new GameObject[4];

            // Calculate camera dimensions
            float screenHeight = 2f * mainCamera.orthographicSize;
            float screenWidth = screenHeight * mainCamera.aspect;

            // Calculate sprite dimensions
            float spriteHeight = sprite.bounds.size.y;
            float spriteWidth = sprite.bounds.size.x;

            // Scale to match camera height exactly
            float scaleY = screenHeight / spriteHeight;
            float scaleX = scaleY; // Use same scale for both X and Y to maintain aspect ratio
            
            // Calculate the actual width after scaling
            float scaledWidth = spriteWidth * scaleX;

            for (int i = 0; i < 4; i++)
            {
                GameObject obj = new GameObject(layer.spriteName + "_" + i);
                obj.transform.parent = this.transform;

                SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
                sr.sprite = sprite;
                sr.sortingOrder = -layers.Length + System.Array.IndexOf(layers, layer); // sky in back

                // Apply uniform scale to fill the camera view
                obj.transform.localScale = new Vector3(scaleX, scaleY, 1);
                
                // Position backgrounds to start from behind the camera and cover the right side
                // Use a small overlap to prevent gaps while minimizing overlap
                float minimalOverlap = scaledWidth * 0.01f; // 1% overlap
                // Start from the left edge of camera view and position each part sequentially
                float cameraLeft = mainCamera.transform.position.x - (mainCamera.orthographicSize * mainCamera.aspect);
                float startX = cameraLeft - scaledWidth + (i * (scaledWidth - minimalOverlap));
                // Align background center with camera center
                float yPosition = mainCamera.transform.position.y;
                obj.transform.position = new Vector3(startX, yPosition, 0);

                layer.parts[i] = obj;
            }
        }
    }

    void Update()
    {
        ParallaxEffect();
    }

    void ParallaxEffect()
    {
        // Check if player is moving horizontally and in which direction
        bool playerIsMovingHorizontally = false;
        float movementDirection = 0f; // -1 for left, +1 for right
        
        if (player != null)
        {
            float horizontalMovement = player.position.x - lastPlayerPosition.x;
            float movementThreshold = 0.01f; // Adjust this value as needed
            
            if (Mathf.Abs(horizontalMovement) > movementThreshold)
            {
                playerIsMovingHorizontally = true;
                movementDirection = Mathf.Sign(horizontalMovement); // Get direction (-1 or +1)
            }
            
            lastPlayerPosition = player.position;
        }

        foreach (var layer in layers)
        {
            if (layer.parts == null || layer.parts.Length == 0) continue;

            // Get camera bounds for repositioning
            float cameraLeftEdge = mainCamera.transform.position.x - (mainCamera.orthographicSize * mainCamera.aspect);
            float cameraRightEdge = mainCamera.transform.position.x + (mainCamera.orthographicSize * mainCamera.aspect);

            // Only scroll if player is moving horizontally
            if (playerIsMovingHorizontally)
            {
                foreach (GameObject part in layer.parts)
                {
                    if (part == null) continue;
                    
                    // Move the background in opposite direction to player movement
                    // If player moves right (+1), background moves left (Vector3.left)
                    // If player moves left (-1), background moves right (Vector3.right)
                    Vector3 scrollDirection = movementDirection > 0 ? Vector3.left : Vector3.right;
                    part.transform.position += scrollDirection * layer.scrollSpeed * Time.deltaTime;
                }
            }

            if (layer.parts[0] != null)
            {
                float width = layer.parts[0].GetComponent<SpriteRenderer>().bounds.size.x;
                float minimalOverlap = width * 0.01f; // 1% overlap to prevent gaps
                float bufferDistance = width * 0.5f; // Extra distance to ensure complete disappearance

                // Reposition if off-screen (both left and right sides)
                for (int i = 0; i < layer.parts.Length; i++)
                {
                    GameObject part = layer.parts[i];
                    if (part == null) continue;

                    // If the part has moved completely off the left side of the camera with buffer
                    if (part.transform.position.x + width/2 < cameraLeftEdge - bufferDistance)
                    {
                        // Find the rightmost position of all parts
                        float rightMost = cameraLeftEdge;
                        foreach (var p in layer.parts)
                        {
                            if (p != null && p.transform.position.x > rightMost)
                                rightMost = p.transform.position.x;
                        }

                        // Position this part to the right of the rightmost part with minimal overlap
                        float yPosition = mainCamera.transform.position.y;
                        part.transform.position = new Vector3(rightMost + width - minimalOverlap, yPosition, part.transform.position.z);
                    }
                    // If the part has moved completely off the right side of the camera with buffer
                    else if (part.transform.position.x - width/2 > cameraRightEdge + bufferDistance)
                    {
                        // Find the leftmost position of all parts
                        float leftMost = cameraRightEdge;
                        foreach (var p in layer.parts)
                        {
                            if (p != null && p.transform.position.x < leftMost)
                                leftMost = p.transform.position.x;
                        }

                        // Position this part to the left of the leftmost part with minimal overlap
                        float yPosition = mainCamera.transform.position.y;
                        part.transform.position = new Vector3(leftMost - width + minimalOverlap, yPosition, part.transform.position.z);
                    }
                }
            }
        }
    }
}
