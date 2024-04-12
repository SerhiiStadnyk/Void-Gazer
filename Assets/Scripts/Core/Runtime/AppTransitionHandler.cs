using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

//TODO: Rename to SceneTransitionHandler
//TODO: Add extended logic with scene groups
//TODO: Add logic for keeping scene loading depending on they group
namespace Core.Runtime
{
    public class AppTransitionHandler : MonoBehaviour
    {
        [SerializeField]
        private SceneReference _mainMenuScene;

        [SerializeField]
        private SceneLifetimeHandler _globalSceneHandler;

        private SceneLifetimeHandlersContainer _sceneLifetimeHandlersContainer;


        [Inject]
        public void Inject(SceneLifetimeHandlersContainer sceneLifetimeHandlersContainer)
        {
            _sceneLifetimeHandlersContainer = sceneLifetimeHandlersContainer;
        }


        public void LoadScene(SceneReference sceneRef)
        {
            //TODO: Move to SceneReference
            Object sceneObject = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(sceneRef.SceneId));

            for (int i = _sceneLifetimeHandlersContainer.LifetimeHandlers.Count - 1; i >= 0; i--)
            {
                SceneLifetimeHandler sceneLifetime = _sceneLifetimeHandlersContainer.LifetimeHandlers[i];
                if (sceneLifetime != _globalSceneHandler)
                {
                    sceneLifetime.CloseScene(SceneLifetimeHandler.SceneClosingType.Discard);
                }
            }

            //TODO: Add scene name to SceneReference
            SceneManager.LoadScene(sceneObject.name);
        }


        public void SwitchScene(SceneReference sceneRef)
        {
            Object sceneObject = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(sceneRef.SceneId));

            for (int i = _sceneLifetimeHandlersContainer.LifetimeHandlers.Count - 1; i >= 0; i--)
            {
                SceneLifetimeHandler sceneLifetime = _sceneLifetimeHandlersContainer.LifetimeHandlers[i];
                if (sceneLifetime != _globalSceneHandler)
                {
                    sceneLifetime.CloseScene(SceneLifetimeHandler.SceneClosingType.Unload);
                }
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
}