using System;
using System.Collections.Generic;
using System.Linq;
using Forms;
using UnityEngine;

[CreateAssetMenu(fileName = "FormsMap", menuName = "Game/Maps/Forms Map", order = 1)]
public class FormsMap : ScriptableObject
{
    [SerializeField]
    private List<FormPair> _forms;


    public BaseForm GetForm(string formId)
    {
        return _forms.First(pair => (pair.Form.FormId == formId)).Form;
    }


    public GameObject GetFormPrefab(string formId)
    {
        return _forms.First(pair => (pair.Form.FormId == formId)).Prefab;
    }


    public GameObject GetFormPrefab(BaseForm form)
    {
        return _forms.First(pair => (pair.Form == form)).Prefab;
    }


    [Serializable]
    private class FormPair
    {
        [SerializeField]
        private BaseForm _form;

        [SerializeField]
        private GameObject _prefab;

        public BaseForm Form => _form;

        public GameObject Prefab => _prefab;
    }
}