using Modules.MutatronicCore.Scripts.Attributes;
using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Runtime.Forms
{
    public abstract class Form: ScriptableObject
    {
        [SerializeField]
        [ReadOnlyField]
        private string _modelName = "Model";

        [SerializeField]
        private string _formName;

        [SerializeField]
        private GameObject _viewPrefab;

        [SerializeField]
        private GameObject _modelPrefab;

        public GameObject ViewPrefab => _viewPrefab;

        public GameObject ModelPrefab => _modelPrefab;

        public string ModelName => _modelName;

        public string FormName => _formName;
    }
}