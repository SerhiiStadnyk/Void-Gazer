using Modules.MutatronicCore.Scripts.Attributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modules.MutatronicCore.Scripts.Runtime.Forms
{
    public abstract class Form: ScriptableObject
    {
        [SerializeField]
        [ReadOnlyField]
        private string _modelName = "Model";

        [FormerlySerializedAs("_formName")]
        [SerializeField]
        private string _formId;

        [SerializeField]
        private GameObject _viewPrefab;

        [SerializeField]
        private GameObject _modelPrefab;

        public GameObject ViewPrefab => _viewPrefab;

        public GameObject ModelPrefab => _modelPrefab;

        public string ModelName => _modelName;

        public string FormId => _formId;

        public string LocalizationName => _formId;
    }
}