using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixselCrew.Utils;
using System.Linq;

namespace PixselCrew.Creatures
{
    public class TotemTower : MonoBehaviour
    {
        [SerializeField] private List<ShootingTrapAI> _traps;
        [SerializeField] private Cooldown _cooldown;
        private int _currentTrap;

        private void Start()
        {
            foreach (var t in _traps)
            {
                t.enabled = false;
                var hp = t.GetComponent<HealthComponent>();
                hp._onDie.AddListener(() => onTrapDaed(t));

            }
        }

        private void onTrapDaed(ShootingTrapAI st)
        {
            var index = _traps.IndexOf(st);
            _traps.Remove(st);
            if (index < _currentTrap)
            {
                _currentTrap--;
            }
        }

        private void Update()
        {
            if (_traps.Count == 0)
            {
                enabled = false;
                Destroy(gameObject, 1F);
            }

            var hasAnyTarget = _traps.Any(x => x._vision.IsTouchingLayer);
            if (hasAnyTarget)
                if (_cooldown.IsReady)
                {
                    _traps[_currentTrap].Shoot();
                    _cooldown.Reset();
                    _currentTrap = (int)Mathf.Repeat(_currentTrap + 1, _traps.Count);
                }
        }
    }
}
