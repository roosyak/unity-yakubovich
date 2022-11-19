using System.Collections;
using UnityEngine;

namespace PixselCrew.Creatures.Weapons
{
    public class Projectile : BaseProjectile
    { 
        protected override void Start()
        {
            base.Start();
            // для Dynamic
            var force = new Vector2(Direction * Speed, 0);
            Rigidbody.AddForce(force, ForceMode2D.Impulse);
        }

        /*
          // для Kinematic
         private void FixedUpdate()
         {
             var position = _rigidbody.position;
             position.x += _direction * _speed;
             _rigidbody.MovePosition(position);
         }*/
    }
}