using UnityEngine;
using PixselCrew.Creatures;
using PixselCrew.Model;

namespace PixselCrew.Components
{
    /*
     общий компонент добавления произвольного объекта к герою 
     */
    public class InventoryAddComponent : MonoBehaviour
    {
        [InventoryIdAttribut] [SerializeField] private string _id;
        [SerializeField] private int _value;

        public void Add(GameObject go)
        {
            var hero = go.GetInterface<ICanAddInInventory>();
            hero?.AddInInventory(_id, _value);
        }
    }
}
