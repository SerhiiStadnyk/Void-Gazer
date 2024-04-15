using System.Collections.Generic;
using Core.Runtime.Forms;
using Core.Runtime.Maps;
using Core.Runtime.Serializable;
using UnityEngine;
using Zenject;

namespace Core.Runtime.Synchronizable
{
    public class SyncFormSpawner : MonoBehaviour
    {
        private Instantiator _instantiator;
        private FormsCollection _formsCollection;
        private SceneSyncHandler _sceneSyncHandler;


        [Inject]
        public void Inject(Instantiator instantiator, FormsCollection formsCollection)
        {
            _instantiator = instantiator;
            _formsCollection = formsCollection;
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
                        BaseForm form = _formsCollection.GetForm<BaseForm>(formId);
                        _instantiator.Instantiate(form, instanceId);
                    }
                }
            }
        }
    }
}
