using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Runtime.Forms
{
    public interface IFormObjectReference
    {
        public GameObject FormObject { get; }

        public Form BaseForm { get; }

        public void SetForm(Form form, FormObjectReferenceInstantiator instantiator);
    }
}