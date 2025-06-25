using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenue : MonoBehaviour
{
    void Update()
    {
        _OpenEscMenue();
    }

    void _OpenEscMenue()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Time.timeScale = 0f; //This pauses the game
            SceneManager.LoadScene("Esc_Scene", LoadSceneMode.Additive);
        }
    }
}
