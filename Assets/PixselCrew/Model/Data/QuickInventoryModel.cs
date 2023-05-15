using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixselCrew.Utils.Disposables;

/*
 модель инвенторя быстрого доступа, хранение и обработка 
*/

namespace PixselCrew.Model
{
    public class QuickInventoryModel
    {
        private readonly PlayerData _data;
        public InventoryItemData[] Inventory { get; private set; }

        public readonly IntProperty SelectedIndex = new IntProperty();

        // событие изменения для внешних «подписчиков»
        public event Action OnChanged;

        // текущий выбранный элемент 
        public InventoryItemData SelectedItem => Inventory[SelectedIndex.Value];

        public QuickInventoryModel(PlayerData data)
        {
            this._data = data;
            Inventory = _data.Inventory.GetAll(ItemTag.Usable);

            // подписка на изменение инвенторя 
            _data.Inventory.OnChanged += OnChangedInventory;
        }


        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        private void OnChangedInventory(string id, int value)
        {
            /*
             проверяем нахождение объекта в «быстром инвенторе» 
             отсеиваем объекты из общего инвенторя
             */
            var indexFound = Array.FindIndex(Inventory, x => x.Id == id);
            if (indexFound != -1)
            {
                Inventory = _data.Inventory.GetAll(ItemTag.Usable);
                SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
                OnChanged?.Invoke();
            }

        }

        public void SetNextItem()
        {
            SelectedIndex.Value = (int)Mathf.Repeat(SelectedIndex.Value + 1, Inventory.Length);
        }
    }
}