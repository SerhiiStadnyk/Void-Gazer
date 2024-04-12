using Core.Inspector;
using UnityEngine;

namespace Core.Runtime.Forms
{
    public abstract class BaseFormInstance : MonoBehaviour, IInstanceIdHolder
    {
        [SerializeField]
        [ReadOnly]
        private string _instanceId;

        public abstract BaseForm BaseForm { get; }

        public string InstanceId
        {
            get { return _instanceId; }
            set { _instanceId = value; }
        }
    }
}
