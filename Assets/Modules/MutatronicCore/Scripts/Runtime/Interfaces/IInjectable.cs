namespace Modules.MutatronicCore.Scripts.Runtime.Interfaces
{
    public interface IInjectable<in T>
    {
        public void InjectDependencies(T container);
    }
}
