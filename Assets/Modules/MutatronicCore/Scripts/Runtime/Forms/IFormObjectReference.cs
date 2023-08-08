using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Runtime.Forms
{
    public interface IFormObjectReference
    {
        public GameObject FormObject { get; }

        public void SetForm(Form form);
    }
}