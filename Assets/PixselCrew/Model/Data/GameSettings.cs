using System;
using UnityEngine;

namespace PixselCrew.Model
{
    /*
     сохранение настроек игры 
     */
    [CreateAssetMenu(menuName = "Data/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private FloatPersistenProperty _music;
        [SerializeField] private FloatPersistenProperty _sfx;

        public FloatPersistenProperty Music => _music;
        public FloatPersistenProperty Sfx => _sfx;

        private static GameSettings _instance;
        public static GameSettings I => _instance == null ? LoadGameSettings() : _instance;

        private static GameSettings LoadGameSettings()
        {
            return _instance = Resources.Load<GameSettings>("GameSettings");
        }

        private void OnEnable()
        {
            _music = new FloatPersistenProperty(1, SoundSetting.Music.ToString());
            _sfx = new FloatPersistenProperty(1, SoundSetting.Sfx.ToString());
        }

        private void OnValidate()
        {
            Music.Validate();
            Sfx.Validate();
        }
    }
    public enum SoundSetting
    {
        Music,
        Sfx
    }
}

