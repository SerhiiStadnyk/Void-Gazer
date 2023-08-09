using UnityEditor;
using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Runtime.Forms
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
    public partial class FormObjectReference<T>
    {
        private Form _internalForm;


        protected void OnValidate()
        {
            if (gameObject.scene.IsValid() && !Application.isPlaying)
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

                    EditorApplication.delayCall += SetupPartsEditor;
                }
                else if(_view != null)
                {
                    EditorApplication.delayCall += DestroyPartsEditor;
                }
            }
        }


        private void SetupPartsEditor()
        {
            DestroyPartsEditor();
            CreateParts();
            HideParts();
        }


        private void DestroyPartsEditor()
        {
            if (_view != null)
            {
                DestroyImmediate(_view);
                _view = null;
                _model = null;
            }
        }
    }
#endif
}
