using UnityEngine;
using PixselCrew.Creatures;

namespace PixselCrew.Components
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [SerializeField] private string _id;
        [SerializeField] private int _value;

        public void Add(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if (hero != null)
                hero.AddInInventory(_id, _value);
        }
    }
}
