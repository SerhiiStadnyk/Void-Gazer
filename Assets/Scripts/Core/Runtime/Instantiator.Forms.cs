using Core.Runtime.Forms;
using UnityEngine;
using Zenject;

namespace Core.Runtime
{
    public partial class Instantiator
    {
        private IdManager _idManager;


        [Inject]
        public void Inject(IdManager idManager)
        {
            _idManager = idManager;
        }


        public GameObject Instantiate(BaseForm form, string id = null)
        {
            GameObject prefab = form.Prefab;
            return InstantiateInternal(prefab, Vector3.zero, Quaternion.identity, _defaultContainer, (obj) => SetInstanceId(obj, id));
        }


        public GameObject Instantiate(BaseForm form, int quantity, string id = null)
        {
            GameObject prefab = form.Prefab;
            return InstantiateInternal(prefab, Vector3.zero, Quaternion.identity, _defaultContainer, (obj) => SetInstanceId(obj, id));
        }


        public GameObject InstantiateAtPointer(BaseForm form)
        {
            GameObject prefab = form.Prefab;
            return InstantiateAtPointerInternal(prefab, (obj) => SetInstanceId(obj));
        }


        private void SetInstanceId(GameObject obj, string instanceId = null)
        {
            IInstanceIdHolder instanceIdHolder = obj.GetComponent<IInstanceIdHolder>();
            if (instanceIdHolder != null)
            {
                instanceId ??= _idManager.GenerateGuid().ToString();
                instanceIdHolder.InstanceId = instanceId;
            }
        }
    }
}
