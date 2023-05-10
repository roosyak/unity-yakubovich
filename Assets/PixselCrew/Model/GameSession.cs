using System;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace PixselCrew.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;

        // сохраненное состояние героя
        private PlayerData _save;

        public QuickInventoryModel QuickInventory { get; private set; }

        private void Awake()
        {
            LoadHud();

            if (IsSessionExit())
            {
                // удаляем из текущей сцены 
                Destroy(gameObject);
            }
            else
            {
                Save();
                InitModel();
                // помещаем объект в «хранилище» которое не удаляется между сценами 
                DontDestroyOnLoad(this);
            }
        }

        private void InitModel()
        {
           QuickInventory = new QuickInventoryModel(Data);
        }

        private void LoadHud()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
        }

        public void LoadLastSave()
        {
            if (_save != null)
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
