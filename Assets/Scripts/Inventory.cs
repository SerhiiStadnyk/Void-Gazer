using System;
using System.Collections.Generic;
using Forms;
using UnityEngine;
using Zenject;

public class Inventory : MonoBehaviour
{
    private Dictionary<ItemForm, int> _items = new Dictionary<ItemForm, int>();

    public event Action<ItemFormInstance> OnItemAdded;

    public Dictionary<ItemForm, int> Items => _items;

    private Instantiator _instantiator;


    [Inject]
    public void Inject(Instantiator instantiator)
    {
        _instantiator = instantiator;
    }


    public bool AddItem(ItemForm item, int quantity)
    {
        bool result = false;

        if (CanAddItem(item))
        {
            if (!Items.ContainsKey(item))
            {
                Items.Add(item, 0);
            }

            Items[item] += quantity;
            result = true;
        }

        return result;
    }


    public bool PickupItem(ItemFormInstance itemInstance)
    {
        bool resul = AddItem(itemInstance.Form, itemInstance.Quantity);
        if (resul)
        {
            OnItemAdded?.Invoke(itemInstance);
        }

        return resul;
    }


    public int RemoveItem(ItemForm item, int quantity)
    {
        int removedCount = 0;

        if (Items.ContainsKey(item))
        {
            removedCount = Math.Min(quantity, Items[item]);
            Items[item] -= removedCount;
        }

        if (Items[item] <= 0)
        {
            Items.Remove(item);
        }

        return removedCount;
    }


    public void DropItem(ItemForm item, int quantity)
    {
        int removedCount = RemoveItem(item, quantity);
        GameObject itemInstance = _instantiator.Instantiate(item, removedCount);
        itemInstance.transform.position = transform.position;
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