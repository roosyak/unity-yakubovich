using UnityEngine;
using PixselCrew.Utils.Disposables;
using PixselCrew.Model;
using System;
using PixselCrew.UI.Hud.QuickInventory;
using System.Collections.Generic;

namespace PixselCrew.UI
{
    public class QuickInventoryController : MonoBehaviour
    {
        [SerializeField] private Transform _conteiner;
        [SerializeField] private InventoryItemWidget _prefab;
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private GameSession _session;
        private InventoryItemData[] _inventory;
        private List<InventoryItemWidget> _createdItem = new List<InventoryItemWidget>();

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            // subscribe model 
            Rebuild();
        }

        private void Rebuild()
        {
            _inventory = _session.Data.Inventory.GetAll();

            // создать не достающие элементы 
            for (var i = _createdItem.Count; i < _inventory.Length; i++)
            {
                var item = Instantiate(_prefab, _conteiner);
                _createdItem.Add(item);
            }

            // обновляем данные в созданных элементах 
            for (var i = 0; i < _inventory.Length; i++)
            {
                _createdItem[i].SetData(_inventory[i], i);
                _createdItem[i].gameObject.SetActive(true);
            }

            // спрятать несипользованные элементы
            for (var i = _inventory.Length; i < _createdItem.Count; i++)
            {
                _createdItem[i].gameObject.SetActive(false);
            }

        }
    }
}
