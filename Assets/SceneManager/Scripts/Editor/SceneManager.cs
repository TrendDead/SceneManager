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
        public Vector2 scrollPosition = Vector2.zero;
        private SceneManager newWindow;

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

            scrollPosition = GUI.BeginScrollView(new Rect(0, 0, Screen.width, Screen.height), scrollPosition, new Rect(0, 0, 0, 23f * scenePaths.Length));

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
                if (GUILayout.Button(Path.GetFileNameWithoutExtension(scenePaths[i]), GUILayout.Width(Screen.width / 2 - 10), GUILayout.Height(20)))
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
                if (GUILayout.Button("Additive", GUILayout.Width(Screen.width / 2 - 10), GUILayout.Height(20)))
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

            GUI.EndScrollView();
        }
    }
}
