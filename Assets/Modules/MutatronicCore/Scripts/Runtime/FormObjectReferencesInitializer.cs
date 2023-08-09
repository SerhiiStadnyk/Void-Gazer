using System.Collections.Generic;
using System.Linq;
using Modules.MutatronicCore.Scripts.Runtime.Forms;
using UnityEngine;
using Zenject;

namespace Modules.MutatronicCore.Scripts.Runtime
{
    public class FormObjectReferencesInitializer : MutatronicBehaviour
    {
        private FormObjectReferenceInstantiator _formRefInstantiator;


        [Inject]
        public void Inject(FormObjectReferenceInstantiator formRefInstantiator)
        {
            _formRefInstantiator = formRefInstantiator;
        }


        protected void Start()
        {
            // List<IFormObjectReference> formRefs = FindObjectsOfType<Component>()
            //     .Select(component => component as IFormObjectReference)
            //     .Where(barComponent => barComponent != null)
            //     .ToList();
            //
            //
            // foreach (IFormObjectReference formRef in formRefs)
            // {
            //     _formRefInstantiator.SetupFormRef(formRef.BaseForm, );
            // }
        }
    }
}
