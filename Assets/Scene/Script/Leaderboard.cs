using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("Start_Screen");
        }
    }
    public void _CloseLeaderboard()
    {
        SceneManager.LoadScene("Start_Screen");
    }
}
