using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Runtime.Forms
{
    public class FormObjectReferenceInstantiator
    {
        public IFormObjectReference Instantiate(Form form, Vector3 position, Quaternion rotation, Transform parent)
        {
            return InstantiateInternal(form, position, rotation, parent);
        }


        public T1 Instantiate<T1, T2>(T2 form, Vector3 position, Quaternion rotation, Transform parent) where T1: FormObjectReference<T2> where T2 : Form
        {
            T1 formRef = InstantiateInternal<T1, T2>(form, position, rotation, parent);
            return formRef;
        }


        public void DisposeFormReference(IFormObjectReference formRef)
        {
            Object.Destroy(formRef.FormObject);
        }


        private IFormObjectReference InstantiateInternal(Form form, Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject formObject = CreateFormObject(form, position, rotation, parent);
            IFormObjectReference formRef = AddFormObjectReferenceComponent(form, formObject);
            SetupFormRef(form, formRef);
            return formRef;
        }


        private T1 InstantiateInternal<T1, T2>(T2 form, Vector3 position, Quaternion rotation, Transform parent) where T1: FormObjectReference<T2> where T2 : Form
        {
            GameObject formObject = CreateFormObject(form, position, rotation, parent);
            T1 formRef = AddFormObjectReferenceComponent<T1, T2>(form, formObject);
            SetupFormRef(form, formRef);
            return formRef;
        }


        private IFormObjectReference AddFormObjectReferenceComponent(Form form, GameObject formObject)
        {
            switch (form)
            {
                case ItemForm:
                    return formObject.AddComponent<ItemFormObjectReference>();
                case StaticForm:
                    return formObject.AddComponent<StaticFormObjectReference>();
                default:
                    return null;
            }
        }


        private T1 AddFormObjectReferenceComponent<T1, T2>(T2 form, GameObject formObject) where T1: FormObjectReference<T2> where T2 : Form
        {
            switch (form)
            {
                case ItemForm:
                    return formObject.AddComponent<ItemFormObjectReference>() as T1;
                case StaticForm:
                    return formObject.AddComponent<StaticFormObjectReference>() as T1;
                default:
                    return null;
            }
        }


        private GameObject CreateFormObject(Form form, Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject result = new GameObject
            {
                transform =
                {
                    position = position,
                    rotation = rotation,
                    parent = parent
                },
                name = form.FormName
            };

            return result;
        }


        public void SetupFormRef(Form form, IFormObjectReference formRef)
        {
            formRef.SetForm(form, this);
        }
    }
}
