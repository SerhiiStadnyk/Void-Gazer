using System;
using Modules.MutatronicCore.Scripts.Runtime.Interfaces;
using UnityEngine.Assertions;

namespace Modules.MutatronicCore.Scripts.Runtime.Forms
{
    public partial class ItemFormObjectReference : FormObjectReference<ItemForm>, IInteractable, IDisposable
    {
        private int _quantity = 1;

        public event Action<IInteractable> OnDispose;


        public void SetQuantity(int quantity)
        {
            Assert.IsTrue(quantity > 0, "Item quantity must be greater than 0!");
            _quantity = quantity;
        }


        void IInteractable.Interact(ActorFormObjectReference actorRef)
        {
            actorRef.Inventory.ReceiveItem(Form, _quantity);
            formObjectReferenceInstantiator.DisposeFormReference(this);
        }


        void IDisposable.Dispose()
        {
            OnDispose?.Invoke(this);
        }
    }
}
