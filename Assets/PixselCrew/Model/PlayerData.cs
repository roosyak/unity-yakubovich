using System;
using UnityEngine;
namespace PixselCrew
{
    [Serializable]
    public class PlayerData
    {
        public int Coins;
        public int Hp;
        public bool IsArmed;

        public PlayerData clone()
        {
            // делаем копию всех полей объекта 
            var j = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(j);
        }
    }
}
