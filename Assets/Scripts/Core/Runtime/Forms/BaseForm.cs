using Core.Inspector;
using UnityEngine;

namespace Core.Runtime.Forms
{
    public abstract class BaseForm : ScriptableObject
    {
        [ReadOnly]
        [SerializeField]
        private string _formId;

        [ReadOnly]
        [SerializeField]
        private GameObject _prefab;

        public string FormId => _formId;

        public GameObject Prefab => _prefab;
    }
}