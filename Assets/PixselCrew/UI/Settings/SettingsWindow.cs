using UnityEngine;
using PixselCrew.Model;
using PixselCrew.UI;

namespace PixselCrew.UI
{
    /*
     привязать элементы формы к модели настроек 
     */
    public class SettingsWindow : AnimatedWindow
    {
        [SerializeField] private AudioSettingsWidget _music;
        [SerializeField] private AudioSettingsWidget _sfx;
        protected override void Start()
        {
            base.Start();
            _music.SetModel(GameSettings.I.Music);
            _sfx.SetModel(GameSettings.I.Sfx);
        }
    }
}
