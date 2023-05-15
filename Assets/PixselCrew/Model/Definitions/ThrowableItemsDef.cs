using UnityEngine;
using System;

namespace PixselCrew.Model
{
    /*
     описание того чем можно кидаться 
     */
    [CreateAssetMenu(menuName = "Defs/ThrowableItemsDef", fileName = "ThrowableItemsDef")]
    public class ThrowableItemsDef : ScriptableObject
    {
        [SerializeField] private ThrowableDef[] _items;

        public ThrowableDef Get(string id)
        {
            foreach (var item in _items)
                if (item.Id == id)
                    return item;

            return default;
        }

    }
    [Serializable]
    public struct ThrowableDef {
        [InventoryIdAttribut] [SerializeField] private string _id;
        [SerializeField] private GameObject _projectile;

        public string Id => _id;
        public GameObject Projectile => _projectile;
    }
}
