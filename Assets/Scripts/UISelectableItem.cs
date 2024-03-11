using System;
using TMPro;
using UnityEngine;

public class UISelectableItem : UISelectableElementDefault
{
    [SerializeField]
    private TMP_Text _itemName;

    [SerializeField]
    private TMP_Text _itemQuantity;


    public void InitElement(Action callback, string itemName, int itemCount)
    {
        base.InitElement(callback);
        _itemName.text = itemName;
        _itemQuantity.text = itemCount.ToString();
    }
}
