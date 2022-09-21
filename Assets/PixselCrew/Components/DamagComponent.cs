using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    /// <summary>
    /// применение урона
    /// </summary>
    public class DamagComponent : MonoBehaviour
    {
        [SerializeField] private int _damag;
        /// <summary>
        /// применить изменения здоровья,
        /// положительные значения — здоровье,
        /// отрицательные — урон
        /// </summary> 
        public void ApplyDamag(GameObject target)
        {
            var healthComponenet = target.GetComponent<HealthComponent>();
            if (healthComponenet != null)
                healthComponenet.ApplyDamage(_damag);
        }
    }
}
