using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("Start_Screen");
        }
    }
    public void _OpenLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void _RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void _ExitGame()
    {
        SceneManager.LoadScene("Start_Screen");
    }

}
