namespace Modules.MutatronicCore.Scripts.Runtime.Scene
{
    public abstract class BaseSceneLifetimeHandler<T1> : MutatronicBehaviour
    {
        public abstract void InjectDependencies(T1 container);
    }
}