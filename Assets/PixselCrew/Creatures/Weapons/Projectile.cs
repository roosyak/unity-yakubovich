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

        public void InvertRT() {

            transform.localScale = new Vector3(transform.localScale.x*-1, 1, 1);
            Speed *= 2;
            Start();

        }
    }
}