using Forms;
using TMPro;
using UnityEngine;

public class UIItemRef : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _itemName;

    [SerializeField]
    private TMP_Text _itemQuantity;

    private Inventory _inventory;


    public void SetupItemRef(Inventory inventory, ItemForm item)
    {
        _inventory = inventory;
        _itemName.text = item.name;
        _itemQuantity.text = _inventory.GetItemCount(item).ToString();
    }
}
