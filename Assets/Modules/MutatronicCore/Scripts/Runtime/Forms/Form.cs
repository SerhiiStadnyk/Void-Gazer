using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Runtime.Forms
{
    public abstract class Form: ScriptableObject
    {
        [SerializeField]
        private GameObject _viewPrefab;

        [SerializeField]
        private string _formName;

        public GameObject ViewPrefab => _viewPrefab;

        public string FormName => _formName;
    }
}