using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Runtime.Forms;
using UnityEngine;

namespace Core.Runtime.Maps
{
    public class FormsCollection : MonoBehaviour
    {
        [SerializeField]
        private string _formsAddressableLabel = "Forms";

        [SerializeField]
        private List<BaseFormMap> _formCollections;


        private async void Start()
        {
            List<Task> loadTasks = new List<Task>();

            foreach (BaseFormMap formCollection in _formCollections)
            {
                Task task = formCollection.LoadAssets(_formsAddressableLabel);
                loadTasks.Add(task);
            }

            await Task.WhenAll(loadTasks);
        }


        public T GetForm<T>(string formId) where T: BaseForm
        {
            T result = null;
            foreach (BaseFormMap formCollection in _formCollections)
            {
                if (formCollection.FormDictionary.ContainsKey(formId))
                {
                    result = formCollection.FormDictionary[formId] as T;
                    break;
                }
            }
            return result;
        }
    }
}