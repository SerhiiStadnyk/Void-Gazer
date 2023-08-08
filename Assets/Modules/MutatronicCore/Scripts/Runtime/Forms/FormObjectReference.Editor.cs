using UnityEditor;
using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Runtime.Forms
{
    [ExecuteInEditMode]
    public partial class FormObjectReference<T>
    {
        private Form _internalForm;


        protected void OnValidate()
        {
            if (gameObject.scene.IsValid())
            {
                if(_form != null)
                {
                    if (_internalForm == null)
                    {
                        _internalForm = _form;
                    }

                    if (_internalForm != _form)
                    {
                        _internalForm = _form;
                    }

                    EditorApplication.delayCall += SetupView;
                }
                else if(view != null)
                {
                    EditorApplication.delayCall += DestroyView;
                }
            }
        }


        private void SetupView()
        {
            DestroyView();
            CreateView();
            HideView();
        }


        private void DestroyView()
        {
            if (view != null)
            {
                DestroyImmediate(view);
            }
        }


        private void CreateView()
        {
            if (view == null && _form != null)
            {
                view = Instantiate(_form.ViewPrefab, Vector3.zero, Quaternion.identity, transform);
            }
        }


        private void HideView()
        {
            if (view != null && view.hideFlags != HideFlags.HideAndDontSave)
            {
                view.hideFlags = HideFlags.HideAndDontSave;
            }
        }
    }
}
