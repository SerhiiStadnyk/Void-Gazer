using Core.Runtime.Serializable;
using Core.Runtime.Synchronizable;
using UnityEngine;
using Zenject;

namespace Core.Runtime.Forms
{
    public class ItemFormInstance : GenericFormInstance<ItemForm>, IInteractable, ISynchronizable
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
                _instantiator.Dispose(gameObject);
            }
        }


        void ISynchronizable.SaveData(Entry entry)
        {
            entry.SetObject(nameof(InstanceId), InstanceId);
            entry.SetObject(nameof(Form.FormId), Form.FormId);
            entry.SetObject(nameof(transform.position), transform.position);
        }


        void ISynchronizable.LoadData(Entry entry)
        {
            InstanceId = entry.GetObject<string>(nameof(InstanceId));
            transform.position = entry.GetObject<Vector3>(nameof(transform.position));
        }
    }
}