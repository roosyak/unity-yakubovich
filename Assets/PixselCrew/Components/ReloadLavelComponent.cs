using UnityEngine;
using UnityEngine.SceneManagement;
namespace PixelCrew.Components
{
    public class ReloadLavelComponent : MonoBehaviour
    {
        public void Reload()
        {
            Debug.Log("SceneManager.LoadScene...");
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}