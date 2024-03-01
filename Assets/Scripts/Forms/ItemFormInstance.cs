using UnityEngine;
using Zenject;

namespace Forms
{
    public class ItemFormInstance : GenericFormInstance<ItemForm>, IInteractable
    {
        [SerializeField]
        private int _quantity;

        private Instantiator _instantiator;

        public bool Interactable => true;

        public int Quantity => _quantity;


        [Inject]
        public void Inject(Instantiator instantiator)
        {
            _instantiator = instantiator;
        }


        public void Interact(ActorFormInstance actor)
        {
            if (actor.ActorInventory.PickupItem(this))
            {
                _instantiator.Dispose(this);
            }
        }
    }
}