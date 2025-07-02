using UnityEngine;

public class CamController : MonoBehaviour
{
    [Header("Target Settings")]
    public GameObject player; // Drag player here in inspector, or leave null to auto-find
    
    [Header("Follow Settings")]
    public float offsetX = 3.5f; // Horizontal offset from the player
    public float fixedY = 6.5f; // Fixed Y position for the camera
    
    [Header("Smoothing")]
    public bool useSmoothing = false; // Disabled by default to prevent blurriness
    public float smoothSpeed = 8f; // Increased speed for less lag
    
    [Header("Bounds (Optional)")]
    public bool useBounds = false;
    public float minX = -50f;
    public float maxX = 50f;

    private void Start()
    {
        // Try to find player if not assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogError("CamController: No player assigned and couldn't find GameObject with 'Player' tag!");
            }
        }
    }

    private void LateUpdate() // Use LateUpdate for camera movement
    {
        if (player == null)
        {
            Debug.LogWarning("CamController: Player reference is null!");
            return;
        }

        // Calculate target position
        Vector3 targetPosition = CalculateTargetPosition();
        
        // Apply bounds if enabled
        if (useBounds)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        }
        
        // Move camera (smoothly or instantly)
        if (useSmoothing)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = targetPosition;
        }
    }
    
    private Vector3 CalculateTargetPosition()
    {
        float targetX = player.transform.position.x + offsetX;
        float targetY = fixedY; // Use fixed Y position instead of following player
        float targetZ = transform.position.z; // Keep original camera Z position
        
        // Round positions to prevent sub-pixel movement (reduces blurriness)
        targetX = Mathf.Round(targetX * 100f) / 100f;
        
        return new Vector3(targetX, targetY, targetZ);
    }
}
