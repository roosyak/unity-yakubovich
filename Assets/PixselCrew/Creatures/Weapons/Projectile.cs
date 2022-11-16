using System.Collections;
using UnityEngine;

namespace PixselCrew.Creatures.Weapons
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField] private float _speed;
        private Rigidbody2D _rigidbody;
        private int _direction;

        private void Start()
        {
            // направление движения меча 
            _direction = transform.lossyScale.x > 0 ? 1 : -1;
            _rigidbody = GetComponent<Rigidbody2D>();

            // для Dynamic
            var force = new Vector2(_direction * _speed, 0);
            _rigidbody.AddForce(force, ForceMode2D.Impulse);
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