using UnityEngine;
using System;

namespace PixselCrew.Model
{
    /*
     описание того чем можно кидаться 
     */
    [CreateAssetMenu(menuName = "Defs/ThrowableItemsDef", fileName = "ThrowableItemsDef")]
    public class ThrowableItemsDef : DefRepository<ThrowableDef>
    {
        
    }
    [Serializable]
    public struct ThrowableDef : IHaveId
    {
        [InventoryIdAttribut][SerializeField] private string _id;
        [SerializeField] private GameObject _projectile;

        public string Id => _id;
        public GameObject Projectile => _projectile;
    }
}
