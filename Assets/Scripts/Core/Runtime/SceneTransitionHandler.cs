using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Zenject;

//TODO: Add extended logic with scene groups
//TODO: Add logic for keeping scene loading depending on they group
namespace Core.Runtime
{
    public class SceneTransitionHandler : MonoBehaviour
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
            CloseScenes(SceneLifetimeHandler.SceneClosingType.Discard);
            Addressables.LoadSceneAsync(sceneRef.ScenePath);
        }


        public void SwitchScene(SceneReference sceneRef)
        {
            CloseScenes(SceneLifetimeHandler.SceneClosingType.Unload);
            Addressables.LoadSceneAsync(sceneRef.ScenePath);
        }


        public void ExitToMainMenu()
        {
            SceneManager.LoadScene(_mainMenuScene.name);
        }


        public void ExitToDesktop()
        {
            Application.Quit();
        }


        private void CloseScenes(SceneLifetimeHandler.SceneClosingType closingType)
        {
            for (int i = _sceneLifetimeHandlersContainer.LifetimeHandlers.Count - 1; i >= 0; i--)
            {
                SceneLifetimeHandler sceneLifetime = _sceneLifetimeHandlersContainer.LifetimeHandlers[i];
                if (sceneLifetime != _globalSceneHandler)
                {
                    sceneLifetime.CloseScene(closingType);
                }
            }
        }
    }
}