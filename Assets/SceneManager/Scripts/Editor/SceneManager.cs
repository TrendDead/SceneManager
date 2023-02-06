using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;
using UnityEditor.SceneManagement;

/// <summary>
/// Окно диспетчера сцен
/// </summary>
public class SceneManager : EditorWindow
{
    [MenuItem("UnityDev/Scene Manager/Creating scene manager window")]
    public static void CreatingWindow()
    {
        EditorWindow.GetWindow<SceneManager>("Scene Manager");
    }

    private void OnGUI()
    {
        string[] scenePaths = EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .Select(scene => scene.path)
                .ToArray();
        
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        for (int i = 0; i < scenePaths.Length; i++)
        {

            if (scene == EditorSceneManager.GetSceneByPath(scenePaths[i]))
            {
                GUI.backgroundColor = Color.green;
            }
            else
            {
                GUI.backgroundColor = Color.white;
            }

            if (GUILayout.Button(Path.GetFileNameWithoutExtension(scenePaths[i])))
            {
                EditorSceneManager.SaveScene(scene);
                EditorSceneManager.OpenScene(scenePaths[i], OpenSceneMode.Single);
            }
        }
    }
}
