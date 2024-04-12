using Core.Runtime.Forms;

namespace Core.Runtime
{
    public interface IInteractable
    {
        public bool Interactable { get; }

        public void Interact(ActorFormInstance actor);
    }
}