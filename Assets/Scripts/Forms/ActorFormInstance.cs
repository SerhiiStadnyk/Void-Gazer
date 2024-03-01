using System;
using Serializable;
using UnityEngine;

namespace Forms
{
    public class ActorFormInstance : GenericFormInstance<ActorForm>, IInitable, ISaveable
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


        void ISaveable.SaveData(Entry entry)
        {
            entry.SetVector3(nameof(transform.position), transform.position);
        }


        void ISaveable.LoadData(Entry entry)
        {
            transform.position = entry.GetVector3(nameof(transform.position));
        }


        void ISaveable.OnLoaded()
        {
            //TODO: IMPLEMENT_ME();
        }


        string ISaveable.GetId => gameObject.GetInstanceID().ToString();
    }
}
