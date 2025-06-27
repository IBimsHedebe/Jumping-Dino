using UnityEngine;

public class CamController : MonoBehaviour
{
    GameObject player;
    public float offsetX = 0f; // Horizontal offset from the player

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogWarning("Player object not found. Make sure the player has the 'Player' tag.");
                return;
            }
        }
        else
        {
            Vector3 playerposition = new Vector3(player.transform.position.x, 0f, 0f);
            Vector3 cameraPosition = new Vector3(playerposition.x + offsetX, transform.position.y, 10f);
            transform.position = cameraPosition;
        }
    }
}
