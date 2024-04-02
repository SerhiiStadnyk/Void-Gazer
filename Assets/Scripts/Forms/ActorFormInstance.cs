using System;
using EditorScripts;
using Serializable;
using Synchronizable;
using UnityEngine;

namespace Forms
{
    public class ActorFormInstance : GenericFormInstance<ActorForm>, IInitable, ISynchronizable
    {
        [SerializeField]
        private float _interactionRadius = 1f;

        [SerializeField]
        private int _interactableLayerIndex;

        [SerializeField]
        [ReadOnly]
        private string _id;
        
        private string _testString;

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


        void ISynchronizable.SaveData(Entry entry)
        {
            entry.SetObject(nameof(transform.position), transform.position);
            //entry.SetObject(nameof(_testString), "CATo");
        }


        void ISynchronizable.LoadData(Entry entry)
        {
            transform.position = entry.GetObject<Vector3>(nameof(transform.position));

            foreach (var keyValuePair in entry.Objects.Dictionary)
            {
                Debug.LogWarning($"{keyValuePair.Key} {keyValuePair.Value}");
            }
            
           //_testString = entry.GetObject<string>(nameof(_testString));
        }
    }
}
