using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using PixselCrew.Utils;

namespace PixselCrew.UI
{
    public class MainMenuWindow : AnimatedWindow
    {
        private Action _cloaseAction;
        public void onShowSettings()
        {
            WindowUtils.CreateWindow("UI/SettingsWindow");
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
