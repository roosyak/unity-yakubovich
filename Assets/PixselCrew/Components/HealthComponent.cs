using UnityEngine;
using UnityEngine.Events;
namespace PixelCrew.Components
{
    /// <summary>
    /// текущее  здоровье 
    /// </summary>
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health = 1;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;

        /// <param name="damageValue"></param>
        public void ApplyDamage(int damageValue)
        {
            _health -= damageValue;
            _onDamage?.Invoke();
            if (_health <= 0)
                _onDie?.Invoke();
        }
    }
}
