using UnityEngine;

namespace Forms
{
    public abstract class BaseForm : ScriptableObject
    {
        [SerializeField]
        private string _formName;

        public string FormName => _formName;
    }
}
