using UnityEngine;
using UnityEngine.SceneManagement;
namespace PixelCrew.Components
{
    public class ReloadLavelComponent : MonoBehaviour
    {
        public void Reload()
        {
            Debug.Log("SceneManager.LoadScene...");
            var session = FindObjectOfType<GameSession>();
            if(session != null)
                //DestroyImmediate(session);
                Destroy(session);
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}