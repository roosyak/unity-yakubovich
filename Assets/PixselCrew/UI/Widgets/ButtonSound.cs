using UnityEngine;
using UnityEngine.EventSystems;

namespace PixselCrew.UI.Widgets
{
    /*
     проиграть звук по нажатию кнопки
     */
    public class ButtonSound : ProbsSound, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            // проиграть зук по клику
            OnPlay();
        }
    }
}