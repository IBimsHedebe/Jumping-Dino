using UnityEngine;
using UnityEngine.SceneManagement;

public class Esc_Menue : MonoBehaviour
{
    public void _Resume()
    {
        Time.timeScale = 1;

    var pauseScene = SceneManager.GetSceneByName("Esc_Scene");
    if (pauseScene.isLoaded)
    {
        SceneManager.UnloadSceneAsync("Esc_Scene");
    }
    else
    {
        Debug.LogWarning("PauseMenu is not loaded!");
    }
    }

    public void _QuitGame()
    {

        SceneManager.LoadScene("Start_Screen");
    }
}
