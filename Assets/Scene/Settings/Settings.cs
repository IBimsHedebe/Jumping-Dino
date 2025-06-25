using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("Start_Screen");
        }
    }
    public void _CloseSettings()
    {
        SceneManager.LoadScene("Start_Screen");
    }
}
