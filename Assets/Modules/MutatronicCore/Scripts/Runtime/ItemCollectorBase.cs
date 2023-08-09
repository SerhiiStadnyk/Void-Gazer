using System.Collections.Generic;
using System.Linq;
using Modules.MutatronicCore.Scripts.Runtime.Forms;
using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Runtime
{
    public class ItemCollectorBase : MutatronicBehaviour
    {
        [SerializeField]
        private ActorFormObjectReference _actorRef;

        private List<ItemFormObjectReference> _proximityItemRefs = new List<ItemFormObjectReference>();


        public void CollectItem()
        {
            _proximityItemRefs = _proximityItemRefs.Where(bar => bar != null).ToList();
            if (_proximityItemRefs.Count > 0)
            {
                _proximityItemRefs[0].CollectItem(_actorRef.Inventory);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            Debug.LogWarning("OnTriggerEnter");
            ItemFormObjectReference item = other.GetComponent<ItemFormObjectReference>();
            if (item != null && !_proximityItemRefs.Contains(item))
            {
                _proximityItemRefs.Add(item);
            }
        }


        private void OnTriggerExit(Collider other)
        {
            Debug.LogWarning("OnTriggerExit");
            ItemFormObjectReference item = other.GetComponent<ItemFormObjectReference>();
            if (item != null && _proximityItemRefs.Contains(item))
            {
                _proximityItemRefs.Remove(item);
            }
        }
    }
}
