using UnityEngine;
using System;

namespace PixselCrew.Model
{
    /*
     описание того чем можно кидаться 
     */
    [CreateAssetMenu(menuName = "Defs/Throwable", fileName = "Throwable")]
    public class ThrowableRepository : DefRepository<ThrowableDef>
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
