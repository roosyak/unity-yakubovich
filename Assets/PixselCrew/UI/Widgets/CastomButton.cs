using UnityEngine;
using UnityEngine.UI;

namespace PixselCrew.UI
{
    // наследник кнопки от базовой кнопки 
    public class CastomButton : Button
    {
        [SerializeField] private GameObject _normal;
        [SerializeField] private GameObject _presed;

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);

            // переключениея между параметрами, при изменении састаяния 
            _normal.SetActive(state != SelectionState.Pressed);
            _presed.SetActive(state == SelectionState.Pressed);
        }
    }
}