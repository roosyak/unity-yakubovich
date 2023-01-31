using UnityEngine;

namespace PixselCrew.Model
{
    [CreateAssetMenu(menuName = "Defs/PlayerDef", fileName = "PlayerDef")]
    public class PlayerDef : ScriptableObject
    {
        // размер инвенторя 
        [SerializeField] private int _inventorySize;

        // максимальное здоровье героя 
        [SerializeField] private int _maxHealth;

        public int InventorySize => _inventorySize;

        public int MaxHealth => _maxHealth;
    }
}
