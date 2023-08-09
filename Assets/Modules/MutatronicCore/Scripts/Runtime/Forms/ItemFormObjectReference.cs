using Modules.MutatronicCore.Scripts.Runtime.Inventory;
using UnityEngine.Assertions;

namespace Modules.MutatronicCore.Scripts.Runtime.Forms
{
    public partial class ItemFormObjectReference : FormObjectReference<ItemForm>
    {
        private int _quantity = 1;


        public void SetQuantity(int quantity)
        {
            Assert.IsTrue(quantity > 0, "Item quantity must be greater than 0!");
            _quantity = quantity;
        }


        public virtual void CollectItem(InventoryBase toInventory)
        {
            toInventory.ReceiveItem(Form, _quantity);
            formObjectReferenceInstantiator.DisposeFormReference(this);
        }
    }
}
