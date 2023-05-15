using UnityEngine;
using PixselCrew.Utils.Disposables;
using PixselCrew.Model;
using System;
using PixselCrew.UI.Hud.QuickInventory;
using System.Collections.Generic;

namespace PixselCrew.UI
{
    /*
     менеджер инвенторя 
     */
    public class QuickInventoryController : MonoBehaviour
    {
        [SerializeField] private Transform _conteiner;
        [SerializeField] private InventoryItemWidget _prefab;
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private GameSession _session; 
        private List<InventoryItemWidget> _createdItem = new List<InventoryItemWidget>();

        private void Start()
        {
            // находим модель «быстрого инвенторя»
            // подписываемя на него и перестраиваем инвентарь
            _session = FindObjectOfType<GameSession>();
            _trash.Retain(_session.QuickInventory.Subscribe(Rebuild)); 
            Rebuild();
        }

        private void Rebuild()
        {
            var inventory = _session.QuickInventory.Inventory;

            // создать не достающие элементы 
            for (var i = _createdItem.Count; i < inventory.Length; i++)
            {
                var item = Instantiate(_prefab, _conteiner);
                _createdItem.Add(item);
            }

            // обновляем данные в созданных элементах 
            for (var i = 0; i < inventory.Length; i++)
            {
                _createdItem[i].SetData(inventory[i], i);
                _createdItem[i].gameObject.SetActive(true);
            }

            // спрятать несипользованные элементы
            for (var i = inventory.Length; i < _createdItem.Count; i++)
            {
                _createdItem[i].gameObject.SetActive(false);
            }

        }
    }
}
