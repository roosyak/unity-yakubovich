using UnityEngine;
using UnityEngine.SceneManagement;
using PixselCrew.Model;

namespace PixselCrew.Components
{
    public class ReloadLavelComponent : MonoBehaviour
    {
        public void Reload()
        {
            Debug.Log("SceneManager.LoadScene...");
            var session = FindObjectOfType<GameSession>();
            session.LoadLastSave();
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}