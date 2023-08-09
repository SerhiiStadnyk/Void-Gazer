using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine;
using Zenject;

namespace Modules.MutatronicCore.Scripts.Runtime.Forms
{
    public abstract partial class FormObjectReference<T> : MutatronicBehaviour, IFormObjectReference where T: Form
    {
        [ReadOnly]
        protected GameObject view;

        [SerializeField]
        protected Form _form;

        protected FormObjectReferenceInstantiator formObjectReferenceInstantiator;

        public T Form => _form as T;

        public Form BaseForm => _form;

        public GameObject FormObject => gameObject;


        [Inject]
        public void Inject(FormObjectReferenceInstantiator formRefInstantiator)
        {
            formObjectReferenceInstantiator ??= formRefInstantiator;
        }


        /// <summary>
        /// Should be called only once on instantiating.
        /// </summary>
        void IFormObjectReference.SetForm([NotNull] Form form, [NotNull]FormObjectReferenceInstantiator instantiator)
        {
            if (_form == null)
            {
                _form = form;
                formObjectReferenceInstantiator = instantiator;
            }
            else
            {
                Debug.LogError("SetForm should be called only once on instantiating");
            }
        }
    }
}