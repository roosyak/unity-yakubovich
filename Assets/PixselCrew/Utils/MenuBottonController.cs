using UnityEngine;
using PixselCrew.Utils;

namespace PixselCrew.UI
{
    /*
     контроллер кнопки в игре, которая вызовет главное меню
     */
    public class MenuBottonController : MonoBehaviour
    {
        public void OnSettings()
        {
            WindowUtils.CreateWindow("UI/MainMenuWindow");
        }
    }
}
