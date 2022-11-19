using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace PixselCrew.Creatures.Weapons
{
    public class BaseProjectile : MonoBehaviour
    {


        [SerializeField] protected float Speed;
        [SerializeField] protected bool InvertX;

        protected Rigidbody2D Rigidbody;
        protected int Direction;


        protected virtual void Start()
        {
            // инвертировать направление  
            var mod = InvertX ? -1 : 1;

            // направление движения «объекта» 
            Direction = mod * transform.lossyScale.x > 0 ? 1 : -1;
            Rigidbody = GetComponent<Rigidbody2D>(); 
        }
    }
}
