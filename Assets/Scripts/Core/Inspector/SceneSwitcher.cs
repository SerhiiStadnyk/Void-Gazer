using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

namespace Core.Inspector
{
    [InitializeOnLoad]
    public class SceneSwitcher
    {
        private static string _sceneEntryPoint = "Assets/Data/Game/Runtime/Scenes/Scene_EntryPoint.unity";
        private static string _sceneMainMenu = "Assets/Data/Game/Runtime/Scenes/Scene_MainMenu.unity";
        private static string _sceneGame = "Assets/Data/Game/Runtime/Scenes/Scene_Game.unity";

        private static string _prefabGlobalContext = "Assets/Resources/ProjectContext.prefab";


        static SceneSwitcher()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }


        static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            SceneButton("EntryPoint", "Load EntryPoint",  _sceneEntryPoint, EditorStyles.miniButtonMid);
            SceneButton("MainMenu", "Load MainMenu",  _sceneMainMenu, EditorStyles.miniButtonMid);
            SceneButton("Game", "Load Game",  _sceneGame, EditorStyles.miniButtonMid);

            PrefabButton("ProjectContext", "Load ProjectContext",  _prefabGlobalContext, EditorStyles.miniButtonRight);
        }


        static void SceneButton(string text, string tooltip, string scenePath, UnityEngine.GUIStyle style)
        {
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(new GUIContent(text, tooltip), style))
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene(scenePath);
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }


        static void PrefabButton(string text, string tooltip, string prefabPath, UnityEngine.GUIStyle style)
        {
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(new GUIContent(text, tooltip), style))
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                Object prefab = AssetDatabase.LoadAssetAtPath<Object>(prefabPath);
                AssetDatabase.OpenAsset(prefab);
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }
    }
}