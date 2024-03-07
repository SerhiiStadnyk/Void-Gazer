using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class AppTransitionHandler : MonoBehaviour
{
    [SerializeField]
    private SceneReference _mainMenuScene;

    private SceneLifetimeHandlersContainer _sceneLifetimeHandlersContainer;


    [Inject]
    public void Inject(SceneLifetimeHandlersContainer sceneLifetimeHandlersContainer)
    {
        _sceneLifetimeHandlersContainer = sceneLifetimeHandlersContainer;
    }


    public void LoadScene(SceneReference sceneRef)
    {
        Object sceneObject = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(sceneRef.SceneId));

        foreach (SceneLifetimeHandler sceneLifetime in _sceneLifetimeHandlersContainer.LifetimeHandlers)
        {
            sceneLifetime.DisposeScene(sceneLifetime.gameObject.scene);
        }

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