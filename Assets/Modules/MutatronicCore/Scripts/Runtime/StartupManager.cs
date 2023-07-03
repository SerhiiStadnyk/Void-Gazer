using Modules.MutatronicCore.Scripts.Runtime.Scene;
using UnityEngine;
using Zenject;

namespace Modules.MutatronicCore.Scripts.Runtime
{
    public class StartupManager : MutatronicBehaviour
    {
        [SerializeField]
        private SceneReference _startingScene;

        private SceneHandler _sceneHandler;


        [Inject]
        public void Inject(SceneHandler sceneHandler)
        {
            _sceneHandler = sceneHandler;
        }


        private void Start()
        {
            //TODO: Loading Screen, SceneRoot initializing
            LoadScenes();
        }


        private void LoadScenes()
        {
            _sceneHandler.StartSceneLoadingSequence(_startingScene);
        }
    }
}
