using Modules.MutatronicCore.Scripts.Runtime.Scene;
using Zenject;

namespace Modules.Game.Scripts
{
    public class SceneLifetimeHandler : BaseSceneLifetimeHandler<DiContainer>
    {
        [Inject]
        public override void InjectDependencies(DiContainer container)
        {
        }
    }
}
