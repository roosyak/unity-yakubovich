using PixselCrew.Model;
using PixselCrew.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace PixselCrew.UI
{
    public class InGameMenuController : AnimatedWindow
    {
        private float _defaultTimeScale;
        public override void Start()
        {
            base.Start();
            _defaultTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        private void OnDestroy()
        {
            Time.timeScale = _defaultTimeScale;
        }

        public void OnShowSetting()
        {
            WindowUtils.CreateWindow("UI/SettingsWindow");
        }
        public void OnExit()
        {
            SceneManager.LoadScene("MainMenu");
            var session = FindObjectOfType<GameSession>();
            Destroy(session.gameObject);
        }
    }

}