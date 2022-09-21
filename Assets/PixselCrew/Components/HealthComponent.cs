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
            _health += damageValue; 
            Debug.Log(string.Format(
                    damageValue > 0 ?
                        "Здоровье: {0} → {1}" :
                        "Урон: {0} → {1}",
                    damageValue,
                    _health)); 

            _onDamage?.Invoke();
            if (_health <= 0)
                _onDie?.Invoke();
        }
    }
}
