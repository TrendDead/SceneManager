using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;
using UnityEditor.SceneManagement;

namespace UnityDev
{
    /// <summary>
    /// ќкно диспетчера сцен
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

                GUILayout.BeginHorizontal();

                if (scene == EditorSceneManager.GetSceneByPath(scenePaths[i]))
                {
                    GUI.backgroundColor = Color.green;
                }
                else
                {
                    GUI.backgroundColor = Color.white;
                }

                // нопка выбора отдельной сцены
                if (GUILayout.Button(Path.GetFileNameWithoutExtension(scenePaths[i])))
                {
                    foreach (var path in scenePaths)
                    {
                        EditorSceneManager.SaveScene(EditorSceneManager.GetSceneByPath(path));
                    }
                    EditorSceneManager.OpenScene(scenePaths[i], OpenSceneMode.Single);
                }

                if (EditorSceneManager.GetSceneByPath(scenePaths[i]).name != null)
                {
                    GUI.backgroundColor = Color.yellow;
                }
                else
                {
                    GUI.backgroundColor = Color.white;
                }

                // нопка совместного открыти€ сцен
                if (GUILayout.Button("Additive"))
                {
                    if (EditorSceneManager.GetSceneByPath(scenePaths[i]).name == null)
                    {
                        EditorSceneManager.OpenScene(scenePaths[i], OpenSceneMode.Additive);
                    }
                    else
                    {
                        EditorSceneManager.SaveScene(EditorSceneManager.GetSceneByPath(scenePaths[i]));
                        EditorSceneManager.UnloadSceneAsync(EditorSceneManager.GetSceneByPath(scenePaths[i]));
                    }
                }

                GUILayout.EndHorizontal();
            }
        }
    }
}
