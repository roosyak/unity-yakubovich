using System.Collections;
using UnityEngine;

namespace PixselCrew.Creatures.Weapons
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField] private float _speed;
        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();  
        }

        private void FixedUpdate()
        {
            
        }
    }
}