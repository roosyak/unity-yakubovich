using System;
using UnityEngine;
namespace PixelCrew
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;

        // сохраненное состояние героя
        private PlayerData _save;

        private void Awake()
        {
            if (IsSessionExit())
            {
                // удаляем из текущей сцены 
                Destroy(gameObject);
            }
            else
            {
                // помещаем объект в «хранилище» которое не удаляется между сценами 
                DontDestroyOnLoad(this);
            }
        }

        public void LoadLastSave()
        {
            _data = _save.clone();
        }

        public void Save()
        {
            _save = _data.clone();
        }

        private bool IsSessionExit()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions)
                if (gameSession != this)
                    return true;

            return false;
        }
    }
}
