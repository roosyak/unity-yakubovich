using UnityEngine;

namespace PixselCrew.Model
{
    [CreateAssetMenu(menuName = "Defs/PlayerDef", fileName = "PlayerDef")]
    public class PlayerDef : ScriptableObject
    {
        // размер инвенторя 
        [SerializeField] private int _inventorySize;

        public int InventorySize => _inventorySize;
    }
}
