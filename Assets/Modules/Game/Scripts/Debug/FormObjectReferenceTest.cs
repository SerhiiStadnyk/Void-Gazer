using UnityEngine;
using Zenject;

namespace Modules.MutatronicCore.Scripts.Runtime.Forms
{
    public class FormObjectReferenceTest : MonoBehaviour
    {
        [SerializeField]
        private ItemForm _form;

        private FormObjectReferenceInstantiator _formRefInstantiator;


        [Inject]
        private void Inject(FormObjectReferenceInstantiator formRefInstantiator)
        {
            _formRefInstantiator = formRefInstantiator;
        }


        void Start()
        {
            IFormObjectReference foo = _formRefInstantiator.Instantiate(_form, Vector3.zero, Quaternion.identity, null);
            ItemFormObjectReference bar = _formRefInstantiator.Instantiate<ItemFormObjectReference, ItemForm>(_form, Vector3.zero, Quaternion.identity, null);

            if (bar == null)
            {
                Debug.LogWarning($"{bar} is null");
            }

            _formRefInstantiator.DisposeFormReference(foo);
            _formRefInstantiator.DisposeFormReference(bar);
        }
    }
}