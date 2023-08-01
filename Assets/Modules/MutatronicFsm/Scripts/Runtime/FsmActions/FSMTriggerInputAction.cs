using HutongGames.PlayMaker;
using Modules.MutatronicCore.Scripts.Runtime.Interfaces;
using Modules.MutatronicCore.Submodules.InputActions.Scripts;
using Zenject;

namespace Modules.MutatronicFsm.Scripts.Runtime.FsmActions
{
    public class FSMTriggerInputAction : FsmStateAction, IInjectable<DiContainer>
    {
        public InputAction inputAction;

        private InputActionsHandler _inputActionsHandler;


        public override void OnEnter()
        {
            _inputActionsHandler.PerformInputAction(inputAction);
        }


        public void InjectDependencies(DiContainer container)
        {
            _inputActionsHandler = container.Resolve<InputActionsHandler>();
        }
    }
}
