﻿using UnityEngine;
using System;
using UnityEditor;

namespace PixselCrew.Model
{
    /*
        Класс описаний предметов (не изменяемые данные)
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
#if UNITY_EDITOR
        // доступ к списку, только в редакторе 
        public ItemDef[] ItemsForEditor => _items;
#endif
    }

    [Serializable]
    public struct ItemDef
    {
        [SerializeField] private string _id;
        [SerializeField] private bool _isStackable;
        public string Id => _id;
        public bool IsStackable => _isStackable;

        public bool IsVoid => string.IsNullOrEmpty(_id);
    }
}
