using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace PixselCrew.UI
{
    public class MainMenuWindow : AnimatedWindow
    {
        private Action _cloaseAction;
        public void onShowSettings()
        {
            var window = Resources.Load<GameObject>("UI/SettingsWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public void OnStartGame()
        {
            _cloaseAction = () =>
            {
                SceneManager.LoadScene("SampleScene");
            };
            Close();
        }
        public void OnExit()
        {
            _cloaseAction = () =>
            {
                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            };
            Close();
        }

        public override void OnCloseAnimationComplete()
        {
            _cloaseAction?.Invoke();
            base.OnCloseAnimationComplete(); 
        }
    }
}
