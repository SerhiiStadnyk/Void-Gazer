using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppTransitionHandler : MonoBehaviour
{
    [SerializeField]
    private SceneAsset _mainMenuScene;


    public void LoadScene(SceneReference sceneRef)
    {
        Object sceneObject = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(sceneRef.SceneId));
        SceneManager.LoadScene(sceneObject.name);
    }


    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(_mainMenuScene.name);
    }


    public void ExitToDesktop()
    {
        Debug.LogWarning("ExitToDesktops");
        Application.Quit();
    }
}