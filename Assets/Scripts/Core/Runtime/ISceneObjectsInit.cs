using UnityEngine;

namespace Core.Runtime
{
    public interface ISceneObjectsInit
    {
        public void InitObject(GameObject obj);

        public void DisposeObject(GameObject obj);
    }
}
