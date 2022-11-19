using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixselCrew.Creatures.Weapons
{
    /*
     движение по сиснусойде 
     */
    public class SinusoidalProjectile : BaseProjectile
    {
        [SerializeField] private float _frequency = 1f; // длинна «волны»
        [SerializeField] private float _amplitude = 1f; // амплитуда
        private float _originalY;
        private float _time;

        protected override void Start()
        {
            base.Start();
            _originalY = Rigidbody.position.y;
        }

        private void FixedUpdate()
        {
            var position = Rigidbody.position;
            position.x += Direction * Speed;
            position.y = _originalY + Mathf.Sin(_time * _frequency) * _amplitude;
            Rigidbody.MovePosition(position);
            _time += Time.fixedDeltaTime;
        }

    }
}
