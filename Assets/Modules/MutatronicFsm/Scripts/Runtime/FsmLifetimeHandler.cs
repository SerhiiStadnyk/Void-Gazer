using HutongGames.PlayMaker;
using Modules.MutatronicCore.Scripts.Runtime;
using Modules.MutatronicCore.Scripts.Runtime.Interfaces;
using Zenject;

namespace Modules.MutatronicFsm.Scripts.Runtime
{
    public class FsmLifetimeHandler : MutatronicBehaviour
    {
        [Inject]
        public void Inject(DiContainer container)
        {
            PlayMakerFSM fsm = GetComponent<PlayMakerFSM>();
            fsm.Preprocess();

            foreach (FsmState state in fsm.FsmStates)
            {
                foreach (FsmStateAction action in state.Actions)
                {
                    if (action is IInjectable<DiContainer> injectable)
                    {
                        injectable.InjectDependencies(container);
                    }
                }
            }
        }
    }
}
