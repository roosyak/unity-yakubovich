using UnityEngine;
using UnityEngine.SceneManagement;
using PixselCrew.Model;
namespace PixselCrew.Components
{
    public class ExitLavelComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        public void Exit()
        {
            var session = FindObjectOfType<GameSession>();
            session.Save();
            SceneManager.LoadScene(_sceneName);
        }
    }
}
