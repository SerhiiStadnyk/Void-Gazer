using System.Collections.Generic;
using Serializable;
using UnityEngine;
using Zenject;

namespace Synchronizable
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
            //TODO: Despawn static objects with Instantiator
        }
    }
}
