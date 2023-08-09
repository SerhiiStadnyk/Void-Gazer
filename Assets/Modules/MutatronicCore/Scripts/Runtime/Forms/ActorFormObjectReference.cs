using Modules.MutatronicCore.Scripts.Runtime.Inventory;
using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Runtime.Forms
{
    public class ActorFormObjectReference : FormObjectReference<ActorForm>
    {
        private InventoryBase _inventory;

        public InventoryBase Inventory => _inventory;


        protected void Awake()
        {
            _inventory = GetComponent<InventoryBase>();
        }
    }
}
