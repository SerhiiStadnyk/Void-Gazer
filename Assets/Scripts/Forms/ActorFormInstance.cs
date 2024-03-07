using System;
using EditorScripts;
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

        [SerializeField]
        [ReadOnly]
        private string _id;

        private CharacterController _charController;
        private Inventory _inventory;

        private int _interactableLayer => 1 << _interactableLayerIndex;

        public CharacterController CharacterController => _charController;
        public Inventory ActorInventory => _inventory;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }


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
            //entry.SetVector3(nameof(transform.position), transform.position);
            entry.SetObject<Vector3>(nameof(transform.position), transform.position);
        }


        void ISaveable.LoadData(Entry entry)
        {
            //transform.position = entry.GetVector3(nameof(transform.position));
            transform.position = entry.GetObject<Vector3>(nameof(transform.position));
        }
    }
}
