using JetBrains.Annotations;
using Modules.MutatronicCore.Scripts.Attributes;
using UnityEngine;
using Zenject;

namespace Modules.MutatronicCore.Scripts.Runtime.Forms
{
    public abstract partial class FormObjectReference<T> : MutatronicBehaviour, IFormObjectReference where T: Form
    {
        [ReadOnlyField]
        [SerializeField]
        private GameObject _view;

        [ReadOnlyField]
        [SerializeField]
        private GameObject _model;

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


        protected void Awake()
        {
            SetupParts();
            AwakeInternal();
        }


        protected virtual void AwakeInternal() { }


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


        private void SetupParts()
        {
            DestroyParts();
            CreateParts();
            HideParts();
        }


        private void DestroyParts()
        {
            if (_view != null)
            {
                Destroy(_view);
                _view = null;
                _model = null;
            }
        }


        private void CreateParts()
        {
            if (_form != null)
            {
                if (_view == null)
                {
                    _view = Instantiate(_form.ViewPrefab, transform.localPosition, Quaternion.identity, transform);
                }

                if (_model == null)
                {
                    _model = Instantiate(_form.ModelPrefab, transform.localPosition, Quaternion.identity, _view.transform);
                    _model.name = _form.ModelName;
                }
            }
        }


        private void HideParts()
        {
            if (_view != null && _view.hideFlags != HideFlags.HideAndDontSave)
            {
                _view.hideFlags = HideFlags.HideAndDontSave;
            }

            if (_model != null && _model.hideFlags != HideFlags.HideAndDontSave)
            {
                _model.hideFlags = HideFlags.HideAndDontSave;
            }
        }
    }
}