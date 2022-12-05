using UnityEngine;
using PixselCrew.Model;
using System.Collections.Generic;

namespace PixselCrew.Components
{
    public class CollectorComponent : MonoBehaviour, ICanAddInInventory
    {
        [SerializeField] private List<InventoryItemData> _items = new List<InventoryItemData>();
        public void AddInInventory(string id, int value)
        {
            _items.Add(new InventoryItemData(id) { Value = value });
        }

        public void DropInInventory()
        {
            var session = FindObjectOfType<GameSession>();
            foreach (var i in _items)
                session.Data.Inventory.Add(i.Id, i.Value);

            _items.Clear();
        }
    }
}
