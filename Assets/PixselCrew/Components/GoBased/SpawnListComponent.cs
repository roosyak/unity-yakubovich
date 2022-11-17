using System;
using System.Linq;
using UnityEngine;

namespace PixselCrew.Components
{
    public class SpawnListComponent : MonoBehaviour
    {
        [SerializeField] private SpawnData[] _spawners;

        public void Spawn(string id)
        {
            var spawners = _spawners.FirstOrDefault(e => e.id == id);
            spawners?.Component.Spawn(); 
        }

        [Serializable]
        public class SpawnData
        {
            public string id;
            public SpawnComponent Component;
        }
    }
}