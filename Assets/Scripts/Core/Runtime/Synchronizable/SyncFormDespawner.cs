using System.Collections.Generic;
using Core.Runtime.Forms;
using Core.Runtime.Serializable;
using UnityEngine;
using Zenject;

namespace Core.Runtime.Synchronizable
{
    public class SyncFormDespawner : MonoBehaviour
    {
        private Instantiator _instantiator;
        private SceneSyncHandler _sceneSyncHandler;


        [Inject]
        public void Inject(Instantiator instantiator)
        {
            _instantiator = instantiator;
        }


        protected void Awake()
        {
            _sceneSyncHandler = GetComponent<SceneSyncHandler>();
            _sceneSyncHandler.OnBeforeLoad += DespawnStaticObjects;
        }


        public void DespawnStaticObjects(Dictionary<string, Entry> dictionary)
        {
            foreach (KeyValuePair<string, ISynchronizable> synchronizable in _sceneSyncHandler.Saveables)
            {
                if (!dictionary.ContainsKey(synchronizable.Key))
                {
                    BaseFormInstance formToDispose = synchronizable.Value as BaseFormInstance;
                    if (formToDispose != null)
                    {
                        _instantiator.Dispose(formToDispose.gameObject);
                    }
                }
            }
        }
    }
}
