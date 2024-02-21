using UnityEngine;

namespace Forms
{
    public abstract class BaseFormInstance<T> : MonoBehaviour where T: BaseForm
    {
        [SerializeField]
        protected T _form;

        public T Form => _form;
    }
}
