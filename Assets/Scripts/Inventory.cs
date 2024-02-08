using System;
using System.Collections.Generic;
using Forms;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<ItemForm, int> _items = new Dictionary<ItemForm, int>();

    public event Action<ItemFormInstance> OnItemAdded;

    public bool AddItem(ItemFormInstance itemInstance, int quantity)
    {
        bool result = false;
        ItemForm item = itemInstance.Form;

        if (CanAddItem(item))
        {
            if (!_items.ContainsKey(item))
            {
                _items.Add(item, 0);
            }

            _items[item] += quantity;
            OnItemAdded?.Invoke(itemInstance);
            result = true;
        }

        return result;
    }


    private bool CanAddItem(ItemForm item)
    {
        return true;
    }
}