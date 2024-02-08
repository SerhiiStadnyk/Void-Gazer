using Forms;

public interface IInteractable
{
    public bool Interactable { get; }

    public void Interact(ActorFormInstance actor);
}