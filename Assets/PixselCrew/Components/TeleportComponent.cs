using UnityEngine;

namespace PixelCrew.Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destTransform;

        public void Teleport(GameObject target)
        {
            Debug.Log("Teleport...");
            target.transform.position = _destTransform.position;
        }
    }

}