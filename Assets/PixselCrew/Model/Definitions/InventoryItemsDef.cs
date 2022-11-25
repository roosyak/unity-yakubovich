﻿using UnityEngine;
using System;

namespace PixselCrew.Model
{
    /*
        Класс описаний предметов 
     */
    [CreateAssetMenu(menuName = "Defs/InventoryItems", fileName = "InventoryItems")]
    public class InventoryItemsDef : ScriptableObject
    {
        [SerializeField] private ItemDef[] _items;

        public ItemDef Get(string id)
        {
            foreach (var item in _items)
                if (item.Id == id)
                    return item;

            return default;
        }
    }

    [Serializable]
    public struct ItemDef
    {
        [SerializeField] private string _id;
        public string Id => _id;

        public bool IsVoid => string.IsNullOrEmpty(_id);
    }
}
