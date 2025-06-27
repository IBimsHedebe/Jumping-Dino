using UnityEngine;

public class Void : MonoBehaviour
{
    [Header("Void Settings")]
    public float voidHeight = -15f; // Y position below which player gets teleported
    public Vector3 respawnPosition = new Vector3(0, 1, 0); // Safe respawn position
    public int damageAmount = 1; // Damage to deal when falling into void
    
    [Header("Player Detection")]
    public GameObject player; // Drag player here, or leave null to auto-find
    
    private HealthSystem healthSystem;

    void Start()
    {
        // Find player if not assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogError("Void: No player found! Make sure player has 'Player' tag or assign manually.");
            }
            else
            {
                Debug.Log("Void: Player found automatically - " + player.name);
            }
        }
        
        // Find health system once at start
        healthSystem = FindFirstObjectByType<HealthSystem>();
        if (healthSystem == null)
        {
            Debug.LogWarning("Void: No HealthSystem found in the scene!");
        }
        
        Debug.Log("Void: Watching for player below Y = " + voidHeight);
        Debug.Log("Void: Respawn position set to: " + respawnPosition);
    }

    void Update()
    {
        // Check if player exists and is below void height
        if (player != null && player.transform.position.y < voidHeight)
        {
            RespawnPlayer();
        }
    }
    
    void RespawnPlayer()
    {
        Debug.Log("Void: Player fell below " + voidHeight + "! Respawning...");
        
        Vector3 oldPosition = player.transform.position;
        player.transform.position = respawnPosition;
        Debug.Log("Player moved from " + oldPosition + " to " + respawnPosition);
        
        // Deal damage if health system exists
        if (healthSystem != null)
        {
            healthSystem.TakeDamage(damageAmount);
            Debug.Log("Void: Dealt " + damageAmount + " damage to player.");
        }
        
        // Reset player velocity if they have a Rigidbody2D
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero; // Stop any falling momentum
            Debug.Log("Void: Reset player velocity.");
        }
        
        Debug.Log("Void: Player respawn complete!");
    }
}
