using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void _StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void _OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void _QuitGame()
    {
        Application.Quit();
    }

    public void _OpenLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
}
