using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [System.Serializable]
    public class BackgroundLayer
    {
        public string spriteName;
        public float scrollSpeed;
        [HideInInspector] public GameObject[] parts;
    }

    public BackgroundLayer[] layers;
    public Camera mainCamera;
    public Transform player; // Reference to the player transform
    
    private Vector3 lastPlayerPosition;
    private Vector3 initialPlayerPosition;

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
        else
        {
            Debug.Log("BackgroundManager: Player assigned - " + player.name);
        }

        // Initialize last player position
        if (player != null)
        {
            lastPlayerPosition = player.position;
            initialPlayerPosition = player.position;
        }

        foreach (var layer in layers)
        {
            Sprite sprite = Resources.Load<Sprite>("comcept Worlds/Green lands/Background/" + layer.spriteName);
            if (sprite == null)
            {
                Debug.LogError("Sprite not found: " + layer.spriteName);
                continue;
            }

            layer.parts = new GameObject[2];

            float screenHeight = 2f * mainCamera.orthographicSize;
            float screenWidth = screenHeight * mainCamera.aspect;

            float spriteHeight = sprite.bounds.size.y;
            float spriteWidth = sprite.bounds.size.x;

            float scale = screenHeight / spriteHeight;
            float scaledWidth = spriteWidth * scale;

            for (int i = 0; i < 2; i++)
            {
                GameObject obj = new GameObject(layer.spriteName + "_" + i);
                obj.transform.parent = this.transform;

                SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
                sr.sprite = sprite;
                sr.sortingOrder = -layers.Length + System.Array.IndexOf(layers, layer);

                // Scale to match screen height
                obj.transform.localScale = new Vector3(scale, scale, 1);
                // Initial positioning
                obj.transform.position = new Vector3(i * scaledWidth, 0, 0);

                layer.parts[i] = obj;
            }
        }
    }

    void Update()
    {
        // Check if player is moving
        bool playerIsMoving = false;
        if (player != null)
        {
            float movementThreshold = 0.01f; // Adjust this value as needed
            playerIsMoving = Vector3.Distance(player.position, lastPlayerPosition) > movementThreshold;
            lastPlayerPosition = player.position;
        }

        foreach (var layer in layers)
        {
            if (layer.parts == null) continue;

            // Update background position to follow player horizontally
            float playerMovementX = 0;
            if (player != null)
            {
                playerMovementX = player.position.x - initialPlayerPosition.x;
            }
            
            foreach (GameObject part in layer.parts)
            {
                Vector3 currentPos = part.transform.position;
                // Move background relative to player's X movement with parallax effect
                float parallaxOffset = playerMovementX * (layer.scrollSpeed / 10f); // Adjust divisor for parallax strength
                part.transform.position = new Vector3(currentPos.x - parallaxOffset * Time.deltaTime, 0, currentPos.z);
                
                // Only scroll if player is moving
                if (playerIsMoving)
                {
                    part.transform.position += Vector3.left * layer.scrollSpeed * Time.deltaTime;
                }
            }

            // Handle wrapping when parts go off screen
            if (layer.parts.Length > 0 && layer.parts[0] != null)
            {
                float width = layer.parts[0].GetComponent<SpriteRenderer>().bounds.size.x;

                for (int i = 0; i < 2; i++)
                {
                    GameObject part = layer.parts[i];
                    if (part.transform.position.x <= -width)
                    {
                        float rightMost = layer.parts[0].transform.position.x;
                        foreach (var p in layer.parts)
                        {
                            if (p.transform.position.x > rightMost)
                                rightMost = p.transform.position.x;
                        }

                        part.transform.position = new Vector3(rightMost + width, 0, 0);
                    }
                }
            }
        }
    }
}
