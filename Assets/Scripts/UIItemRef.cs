using System;
using Forms;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemRef : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _itemName;

    [SerializeField]
    private TMP_Text _itemQuantity;

    [SerializeField]
    private Image _backgroundImage;

    [SerializeField]
    private Button _button;

    [SerializeField]
    private Color _activeColor;

    [SerializeField]
    private Color _inactiveColor;

    private Inventory _inventory;


    public void SetupItemRef(Inventory inventory, ItemForm item, Action<UIItemRef> callback)
    {
        _inventory = inventory;
        _itemName.text = item.name;
        _itemQuantity.text = _inventory.GetItemCount(item).ToString();
        _button.onClick.AddListener(() => callback(this));
    }


    public void SetActive(bool value)
    {
        _backgroundImage.color = value ? _activeColor : _inactiveColor;
    }
}
