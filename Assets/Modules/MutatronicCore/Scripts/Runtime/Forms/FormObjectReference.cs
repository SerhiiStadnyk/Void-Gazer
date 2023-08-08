using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Runtime.Forms
{
    public abstract partial class FormObjectReference<T> : MutatronicBehaviour, IFormObjectReference where T: Form
    {
        [ReadOnly]
        protected GameObject view;

        [SerializeField]
        protected Form _form;

        public T Form => _form as T;

        public GameObject FormObject => gameObject;


        /// <summary>
        /// Should be called only once on instantiating.
        /// </summary>
        void IFormObjectReference.SetForm([NotNull] Form form)
        {
            if (_form == null)
            {
                _form = form;
            }
            else
            {
                Debug.LogError("SetForm should be called only once on instantiating");
            }
        }
    }
}