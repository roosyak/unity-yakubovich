using System;
using UnityEngine;

namespace PixselCrew.Model
{
    [CreateAssetMenu(menuName = "Defs/Potions", fileName = "Potions")]
    public class PotionRepository : DefRepository<PotionDef>
    {

    }


    /*
      структура залья здоровья 
     */
    [Serializable]
    public struct PotionDef : IHaveId
    {

        [InventoryIdAttribut][SerializeField] private string _id;
        [SerializeField] private float _value;
        [SerializeField] private float _time;

        public string Id => _id;
        public float Value => _value;
        public float Time => _time;

    }
}
