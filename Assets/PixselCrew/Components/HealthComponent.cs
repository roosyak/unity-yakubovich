using UnityEngine;
using UnityEngine.Events;
using System;
/// <summary>
/// текущее  здоровье 
/// </summary>
public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _health = 1;
    [SerializeField] private UnityEvent _onDamage;
    [SerializeField] private UnityEvent _onDie;
    [SerializeField] private HealthChangeEvent _onChange;

    /// <summary>
    /// измененить здоровье,
    /// положительные значения — здоровье,
    /// отрицательные — урон
    /// </summary> 
    /// <param name="damageValue"> значение здороья/урона</param>
    public void ApplyDamage(int damageValue)
    {
        _health += damageValue;
        _onChange?.Invoke(_health);

        var log = string.Format(
                damageValue > 0 ?
                    "Здоровье: {0} → {1}" :
                    "Урон: {0} → {1}",
                damageValue,
                _health);

        if (damageValue < 0)
        {
            Debug.Log("_onDamage: " + log);
            _onDamage?.Invoke();
        }

        if (_health <= 0)
        {
            // Debug.Log("_onDie");
            _onDie?.Invoke();
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Update Health")]
    private void UpdateHealth()
    {
        _onChange?.Invoke(_health);
    }
#endif
    public void SetHealth(int health)
    {
        _health = health;
    }

    [Serializable]
    public class HealthChangeEvent : UnityEvent<int>
    {
    }
}
