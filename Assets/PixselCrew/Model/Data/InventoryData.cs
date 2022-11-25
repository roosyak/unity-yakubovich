using UnityEngine;
using System;
using System.Collections.Generic;
using PixselCrew.Model;

namespace PixselCrew.Model
{
    [Serializable]
    public class InventoryData
    {
        [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();

        // описание метода 
        public delegate void OnInventoryChanged(string id, int value);

        // переменная в которую можно записать метод
        public OnInventoryChanged OnChanged;

        public void Add(string id, int value)
        {
            if (value <= 0) return;

            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return;

            var item = GetItem(id);
            if (item == null)
            {
                item = new InventoryItemData(id);
                _inventory.Add(item);
            }
            item.Value += value;

            OnChanged?.Invoke(id, Count(id));
        }

        public void Remove(string id, int value)
        {
            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return;

            var item = GetItem(id);
            if (item == null) return;

            item.Value -= value;

            if (item.Value <= 0)
                _inventory.Remove(item);

            OnChanged?.Invoke(id, Count(id));
        }

        private InventoryItemData GetItem(string id)
        {
            foreach (var item in _inventory)
                if (item.Id == id)
                    return item;

            return null;
        }

        public int Count(string id)
        {
            var count = 0;
            foreach (var item in _inventory)
                if (item.Id == id)
                    count += item.Value;
            return count;
        }
    }

    [Serializable]
    public class InventoryItemData
    {
        [InventoryIdAttribut] public string Id;
        public int Value;

        public InventoryItemData(string id)
        {
            Id = id;
        }
    }
}
