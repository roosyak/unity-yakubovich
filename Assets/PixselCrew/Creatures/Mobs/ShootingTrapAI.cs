using UnityEngine;
using PixselCrew.Utils;
using System;

namespace PixselCrew.Creatures
{
    public class ShootingTrapAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private Cooldown _cooldown;
        [SerializeField] private SpriteAnimation _animation;

        private void Update()
        {
            if (_vision.IsTouchingLayer && _cooldown.IsReady)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            _cooldown.Reset();
            _animation.SetClip("start-attack");
        }
    }
}
 