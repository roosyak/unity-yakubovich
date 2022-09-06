﻿using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
   [SerializeField] private Hero _hero;


    public void OnHorizontalMovement(InputAction.CallbackContext context) {
        var direction = context.ReadValue<float>();
        _hero.SetDirection(direction);
    }
    public void OnSaySomethig(InputAction.CallbackContext context) {
        if (context.canceled)
        {
            _hero.SaySomething();
        }
    }
}
