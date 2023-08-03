using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Runtime
{
    public abstract class FactoryBase<T> : ScriptableObject
    {
        public abstract T GetObject(FactoryPointer pointer);
    }
}
