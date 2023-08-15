using System.Collections.Generic;
using System.Linq;
using Modules.MutatronicCore.Scripts.Runtime.Forms;
using Modules.MutatronicCore.Scripts.Runtime.Interfaces;
using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Runtime
{
    public class FormObjectInteractor : MutatronicBehaviour
    {
        [SerializeField]
        private ActorFormObjectReference _actorRef;

        private List<IInteractable> _proximityInteractableRefs = new List<IInteractable>();

        private IInteractable _chosenInteractable => _proximityInteractableRefs[0];


        public void Interact()
        {
            _proximityInteractableRefs = _proximityInteractableRefs.Where(bar => bar != null).ToList();
            if (_proximityInteractableRefs.Count > 0)
            {
                _chosenInteractable.Interact(_actorRef);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            Debug.LogWarning("OnTriggerEnter");
            if (other.GetComponent(typeof(IInteractable)) is IInteractable interactable &&
                !_proximityInteractableRefs.Contains(interactable))
            {
                _proximityInteractableRefs.Add(interactable);
                interactable.OnDispose += OnInteractableDisposed;
            }
        }


        private void OnTriggerExit(Collider other)
        {
            Debug.LogWarning("OnTriggerExit");
            ItemFormObjectReference interactable = other.GetComponent<ItemFormObjectReference>();
            if (interactable != null && _proximityInteractableRefs.Contains(interactable))
            {
                _proximityInteractableRefs.Remove(interactable);
                interactable.OnDispose -= OnInteractableDisposed;
            }
        }


        private void OnInteractableDisposed(IInteractable interactable)
        {
            if (_proximityInteractableRefs.Count > 0 && _proximityInteractableRefs.Contains(interactable))
            {
                _proximityInteractableRefs.Remove(interactable);
            }
        }
    }
}
