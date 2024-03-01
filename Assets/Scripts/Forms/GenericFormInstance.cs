using UnityEngine;

namespace Forms
{
    public class GenericFormInstance<T> : BaseFormInstance where T: BaseForm
    {
        [SerializeField]
        protected T _form;

        public T Form => _form;
    }
}