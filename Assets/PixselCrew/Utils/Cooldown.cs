using System;
using UnityEngine;

namespace PixselCrew.Utils
{
    [Serializable]
    public class Cooldown
    {
        // сколько секунд ждать 
        [SerializeField] private float _value;

        private float _timesUp;
        public void Reset()
        {
            _timesUp = Time.time + _value;
        }

        public bool IsReady => _timesUp <= Time.time;
    }
}