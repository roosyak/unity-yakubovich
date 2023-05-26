using UnityEngine;
using UnityEngine.InputSystem;

namespace PixselCrew
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private PixselCrew.Creatures.Hero _hero;

        public void OnHorizontalMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }

        public void OnSaySomethig(InputAction.CallbackContext context)
        {
            /* if (context.phase == InputActionPhase.Performed)
                 _hero.SaySomething();
            */
        }

        public void OnInteract(InputAction.CallbackContext context)
        {

            if (context.phase == InputActionPhase.Performed)
                _hero.Interact();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {

            if (context.performed)
                _hero.Attack();
        }

        public void OnThrow(InputAction.CallbackContext context)
        {
            if (context.performed)
                _hero.UseInventory();
        }

        // следующий элемент выстрого инвенторя 
        public void OnNextItem(InputAction.CallbackContext context)
        {
            if (context.performed)
                _hero.NextItem();

        }

    }

}