namespace Forms
{
    public class ItemFormInstance : BaseFormInstance<ItemForm>, IInteractable
    {
        private int _quantity;

        public bool Interactable => true;

        public int Quantity => _quantity;


        public void Interact(ActorFormInstance actor)
        {
            if (actor.ActorInventory.AddItem(this, Quantity))
            {
                Destroy(gameObject);
            }
        }
    }
}