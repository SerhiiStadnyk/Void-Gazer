using UnityEngine;

namespace Core.Runtime.Forms
{
    [CreateAssetMenu(fileName = "ItemForm", menuName = "Game/Forms/Item", order = 1)]
    public class ItemForm : BaseForm
    {
        [Space(10)]
        [SerializeField]
        private AudioClip _pickUpSound;

        [SerializeField]
        private Sprite _itemIcon;

        public AudioClip PickUpSound => _pickUpSound;
        public Sprite ItemIcon => _itemIcon;
    }
}