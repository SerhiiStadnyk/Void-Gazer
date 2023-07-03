using System.Collections.Generic;
using HutongGames.PlayMaker;
using Modules.MutatronicFsm.Scripts.Runtime;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Modules.MutatronicCore.Scripts.Runtime.Scene
{
    public class SceneHandler : MutatronicBehaviour
    {
        [SerializeField]
        private PlayMakerFSM _sceneOperationsSequenceFsm;

        [SerializeField]
        private FsmVariablePointer _sceneToLoadPointer;

        [SerializeField]
        private FsmVariablePointer _startSceneSequencePointer;

        private FsmObject _fsmSceneToLoadVal;
        private Fsm _fsm;

        private Dictionary<SceneReference, SceneInstance> _loadedScenes = new Dictionary<SceneReference, SceneInstance>();
        private bool _isSequenceLoading;

        public bool IsLoading => _isSequenceLoading;


        protected void Start()
        {
            _fsm = _sceneOperationsSequenceFsm.Fsm;
            _fsmSceneToLoadVal = _fsm.GetFsmObject(_sceneToLoadPointer.VariableName);
        }


        public void StartSceneLoadingSequence(SceneReference sceneToLoad)
        {
            Assert.IsNotNull(sceneToLoad, "Scene to load cannot be null!");
            Assert.IsFalse(_loadedScenes.ContainsKey(sceneToLoad), $"Scene {sceneToLoad.name} at path {sceneToLoad.ScenePath} already loaded!");
            Assert.IsFalse(_isSequenceLoading, $"Previous scene is not finished loading! Use {nameof(IsLoading)} to check is loading process finished.");

            _isSequenceLoading = true;
            _fsmSceneToLoadVal.Value = sceneToLoad;
            _fsm.Event(_startSceneSequencePointer.VariableName);
        }


        public bool IsSceneLoaded(SceneReference sceneRef)
        {
            return _loadedScenes.ContainsKey(sceneRef);
        }


        private async void UnloadScene(SceneReference sceneRef)
        {
            if (_loadedScenes.ContainsKey(sceneRef))
            {
                AsyncOperationHandle<SceneInstance> sceneHandle = Addressables.UnloadSceneAsync(_loadedScenes[sceneRef]);
                await sceneHandle.Task;

                _loadedScenes.Remove(sceneRef);
            }
        }


        private async void LoadScene(SceneReference sceneRef)
        {
            AsyncOperationHandle<SceneInstance> sceneHandle = Addressables.LoadSceneAsync(sceneRef.ScenePath, UnityEngine.SceneManagement.LoadSceneMode.Additive);
            await sceneHandle.Task;
            _loadedScenes.Add(sceneRef, sceneHandle.Result);
            _isSequenceLoading = false;
        }
    }
}
