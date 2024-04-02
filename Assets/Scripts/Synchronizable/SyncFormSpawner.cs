using System.Collections.Generic;
using Serializable;
using UnityEngine;
using Zenject;

namespace Synchronizable
{
    public class SyncFormSpawner : MonoBehaviour
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

            _sceneSyncHandler.OnBeforeLoad += SpawnDynamicObjects;
        }


        public void SpawnDynamicObjects(Dictionary<string, Entry> dictionary)
        {
            //TODO: Spawn dynamic objects with Instantiator
        }
    }
}
