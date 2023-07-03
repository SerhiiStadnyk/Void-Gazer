using HutongGames.PlayMaker;
using Modules.MutatronicCore.Scripts.Runtime.Interfaces;
using Modules.MutatronicCore.Scripts.Runtime.Scene;
using Zenject;

namespace Modules.MutatronicFsm.Scripts.Runtime.FsmActions
{
    public class LoadSceneStateAction : FsmStateAction, IInjectable<DiContainer>
    {
        public SceneReference sceneReference;

        private SceneHandler _sceneHandler;


        public void InjectDependencies(DiContainer container)
        {
            _sceneHandler = container.Resolve<SceneHandler>();
        }


        public override void OnEnter()
        {
            _sceneHandler.StartSceneLoadingSequence(sceneReference);
        }
    }
}
