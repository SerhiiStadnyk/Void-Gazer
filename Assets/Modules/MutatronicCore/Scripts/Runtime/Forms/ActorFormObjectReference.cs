using Modules.MutatronicCore.Scripts.Runtime.Inventory;

namespace Modules.MutatronicCore.Scripts.Runtime.Forms
{
    public class ActorFormObjectReference : FormObjectReference<ActorForm>
    {
        private InventoryBase _inventory;

        public InventoryBase Inventory => _inventory;


        protected override void AwakeInternal()
        {
            _inventory = GetComponent<InventoryBase>();
        }
    }
}
