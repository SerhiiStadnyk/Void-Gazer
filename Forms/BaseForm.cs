using UnityEngine;

namespace Forms
{
    public abstract class BaseForm : ScriptableObject
    {
        [SerializeField]
        private string _formId;

        public string FormId => _formId;
    }
}
