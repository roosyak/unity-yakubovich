﻿using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    private Vector2 _direction;

    public void SetDirection(Vector2 direction) {
        _direction = direction;
    }
    private void Update()
    {
        if (_direction.magnitude > 0) {
            var delta = _direction * _speed * Time.deltaTime;
            transform.position += new Vector3(delta.x, delta.y, transform.position.z);
        }
    }
    public void SaySomething() {
        Debug.Log("SaySomething");
    }
}
