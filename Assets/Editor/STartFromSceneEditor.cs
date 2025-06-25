using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class StartFromSceneEditor
{
    private const string startScenePath = "Assets/Scene/Start_Screen/Start_Screen.unity"; // CHANGE THIS PATH

    static StartFromSceneEditor()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            if (EditorSceneManager.GetActiveScene().path != startScenePath)
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(startScenePath);
                }
                else
                {
                    EditorApplication.isPlaying = false;
                }
            }
        }
    }
}
