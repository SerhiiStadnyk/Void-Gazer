using System;
using Modules.MutatronicCore.Scripts.Runtime.Forms;

namespace Modules.MutatronicCore.Scripts.Runtime.Interfaces
{
    public interface IInteractable
    {
        public void Interact(ActorFormObjectReference actorRef);

        public event Action<IInteractable> OnDispose;
    }
}
