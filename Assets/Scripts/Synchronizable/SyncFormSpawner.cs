using System.Collections.Generic;
using Forms;
using Serializable;
using UnityEngine;
using Zenject;

namespace Synchronizable
{
    public class SyncFormSpawner : MonoBehaviour
    {
        [SerializeField]
        private FormsMap _formsMap;

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
            foreach (KeyValuePair<string, Entry> pair in dictionary)
            {
                if (!_sceneSyncHandler.Saveables.ContainsKey(pair.Key))
                {
                    pair.Value.Deserialize();
                    string formId = pair.Value.GetObject<string>(nameof(BaseForm.FormId));
                    if (!string.IsNullOrEmpty(formId))
                    {
                        string instanceId = pair.Value.GetObject<string>(nameof(IInstanceIdHolder.InstanceId));
                        BaseForm form = _formsMap.GetForm(formId);
                        _instantiator.Instantiate(form, instanceId);
                    }
                }
            }
        }
    }
}
