using HutongGames.PlayMaker;
using Modules.MutatronicCore.Scripts.Runtime.Interfaces;
using Modules.MutatronicCore.Scripts.Runtime.Scene;
using Zenject;

namespace Modules.MutatronicFsm.Scripts.Runtime.FsmActions
{
    public class IsSceneActiveStateAction : FsmStateAction, IInjectable<DiContainer>
    {
        public bool everyFrame;
        public SceneReference sceneReference;

        [HutongGames.PlayMaker.Tooltip("Event to send if Scene is active.")]
        public FsmEvent isActive;

        [HutongGames.PlayMaker.Tooltip("Event to send if Scene is NOT active.")]
        public FsmEvent isNotActive;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Store the result in a bool variable.")]
        public FsmBool storeResult;

        private SceneHandler _sceneHandler;


        public void InjectDependencies(DiContainer container)
        {
            _sceneHandler = container.Resolve<SceneHandler>();
        }


        public override void OnEnter()
        {
            IsSceneActive();

            if (!everyFrame)
            {
                Finish();
            }
        }


        public override void OnUpdate()
        {
            IsSceneActive();
        }


        private void IsSceneActive()
        {
            bool isSceneActive = _sceneHandler.IsSceneLoaded(sceneReference);

            if (storeResult != null)
            {
                storeResult.Value = isSceneActive;
            }

            Fsm.Event(isSceneActive ? isActive : isNotActive);
        }
    }
}
