using System;
using System.Collections.Generic;
using Forms;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<ItemForm, int> _items = new Dictionary<ItemForm, int>();

    public event Action<ItemFormInstance> OnItemAdded;

    public Dictionary<ItemForm, int> Items => _items;


    public bool AddItem(ItemFormInstance itemInstance, int quantity)
    {
        bool result = false;
        ItemForm item = itemInstance.Form;

        if (CanAddItem(item))
        {
            if (!Items.ContainsKey(item))
            {
                Items.Add(item, 0);
            }

            Items[item] += quantity;
            OnItemAdded?.Invoke(itemInstance);
            result = true;
        }

        return result;
    }


    public int GetItemCount(ItemForm item)
    {
        int result = 0;

        if (Items.ContainsKey(item))
        {
            result = Items[item];
        }

        return result;
    }


    private bool CanAddItem(ItemForm item)
    {
        return true;
    }
}