using System;
using UnityEngine;

namespace Forms
{
    public class ActorFormInstance : BaseFormInstance<ActorForm>, IInitable
    {
        [SerializeField]
        private float _interactionRadius = 1f;

        [SerializeField]
        private int _interactableLayerIndex;

        private CharacterController _charController;
        private Inventory _inventory;

        private int _interactableLayer => 1 << _interactableLayerIndex;

        public CharacterController CharacterController => _charController;
        public Inventory ActorInventory => _inventory;


        void IInitable.Init()
        {
            _charController = GetComponent<CharacterController>();
            _inventory = GetComponent<Inventory>();
        }


        public void InteractWith()
        {
            Collider[] colliders = new Collider[10];

            int count = Physics.OverlapSphereNonAlloc(transform.position, _interactionRadius, colliders, _interactableLayer);

            for (int i = 0; i < count; i++)
            {
                if (colliders[i] != null)
                {
                    IInteractable interactable = colliders[i].GetComponent<IInteractable>();
                    if (interactable.Interactable)
                    {
                        interactable.Interact(this);
                        Array.Clear(colliders, 0, colliders.Length);
                        return;
                    }
                }
            }

            Array.Clear(colliders, 0, colliders.Length);
        }
    }
}
