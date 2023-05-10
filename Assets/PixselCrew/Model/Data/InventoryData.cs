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


            if (itemDef.IsStackable)
            {
                AddToStack(id, value);
            }
            else
            {
                AddNoStack(id, value);
            }

            OnChanged?.Invoke(id, Count(id));
        }

        public InventoryItemData[] GetAll()
        {
            return _inventory.ToArray();
        }

        private void AddToStack(string id, int value)
        {
            var item = GetItem(id);
            if (item == null)
            {
                var isFull = _inventory.Count >= DefsFacade.I.Player.InventorySize;
                if (isFull) return;

                item = new InventoryItemData(id);
                _inventory.Add(item);
            }
            item.Value += value;
        }

        private void AddNoStack(string id, int value)
        {
            var itemLaset = DefsFacade.I.Player.InventorySize - _inventory.Count;
            value = Mathf.Min(itemLaset, value);
            for (var i = 0; i < value; i++)
            {
                var item = new InventoryItemData(id) { Value = 1 };
                _inventory.Add(item);
            }
        }

        public void Remove(string id, int value)
        {
            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return;

            if (itemDef.IsStackable)
            {
                RemoveFromStack(id, value);
            }
            else
            {

                RemoveNotStack(id, value);
            }

            OnChanged?.Invoke(id, Count(id));
        }

        private void RemoveFromStack(string id, int value)
        {
            var item = GetItem(id);
            if (item == null) return;

            item.Value -= value;

            if (item.Value <= 0)
                _inventory.Remove(item);
        }

        private void RemoveNotStack(string id, int value)
        {
            for (var i = 0; i < value; i++)
            {

                var item = GetItem(id);
                if (item == null) return;
                _inventory.Remove(item);
            }
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
