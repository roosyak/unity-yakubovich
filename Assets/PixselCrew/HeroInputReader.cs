using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
   [SerializeField] private Hero _hero;
    private float directX, directY;

    public void OnHorizontalMovement(InputAction.CallbackContext context) {
        directX = context.ReadValue<float>();
        _hero.SetDirection(directX, directY);
    }
    public void OnVerticalMovement(InputAction.CallbackContext context)
    {
        directY = context.ReadValue<float>();
        _hero.SetDirection(directX, directY);
    }
    public void OnSaySomethig(InputAction.CallbackContext context) {
        if (context.canceled)
        {
            _hero.SaySomething();
        }
    }
}
