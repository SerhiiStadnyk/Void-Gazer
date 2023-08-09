using System;
using System.Collections.Generic;
using Modules.MutatronicCore.Scripts.Runtime.Forms;
using UnityEngine.Assertions;

namespace Modules.MutatronicCore.Scripts.Runtime.Inventory
{
    public class InventoryBase : MutatronicBehaviour
    {
        private Dictionary<ItemForm, int> _itemsList = new Dictionary<ItemForm, int>();

        public IReadOnlyDictionary<ItemForm, int> ItemsList => _itemsList;

        public event Action<InventoryChangedStatus> OnInventoryChanged;

        public enum InventoryChangedStatus
        {
            ItemReceived,
            ItemRemoved
        }


        public virtual void ReceiveItem(ItemForm item, int quantity)
        {
            Assert.IsTrue(quantity > 0, "Cannot add negative or zero items!");
            Assert.IsNotNull(item, "Cannot add null item!");

            if (_itemsList.ContainsKey(item))
            {
                _itemsList[item] += quantity;
            }
            else
            {
                _itemsList.Add(item, quantity);
            }

            OnInventoryChanged?.Invoke(InventoryChangedStatus.ItemReceived);
        }


        public virtual void RemoveItem(ItemForm item, int quantity)
        {
            Assert.IsTrue(quantity > 0, "Cannot remove negative or zero items!");
            Assert.IsNotNull(item, "Cannot remove null item!");

            if (_itemsList.ContainsKey(item))
            {
                if (_itemsList[item] <= quantity)
                {
                    _itemsList.Remove(item);
                }
                else
                {
                    _itemsList[item] -= quantity;
                }

                OnInventoryChanged?.Invoke(InventoryChangedStatus.ItemRemoved);
            }
        }


        public virtual void TransferItem(ItemForm item, int quantity, InventoryBase toInventory)
        {
            Assert.IsTrue(quantity > 0, "Cannot move negative or zero items!");
            Assert.IsNotNull(item, "Cannot move null item!");

            if (_itemsList.ContainsKey(item))
            {
                int actualQuantity = quantity;
                if (_itemsList[item] < quantity)
                {
                    actualQuantity = quantity - (_itemsList[item] - quantity);
                }

                RemoveItem(item, quantity);
                toInventory.ReceiveItem(item, actualQuantity);
            }
        }


        public virtual void DropItem(ItemForm item, int quantity)
        {
            RemoveItem(item, quantity);
        }
    }
}