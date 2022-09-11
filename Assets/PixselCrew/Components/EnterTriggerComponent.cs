using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        /// <summary>
        /// тег с которым срабатывает событие 
        /// </summary>
        [SerializeField] private string _tag;

        /// <summary>
        /// внешний метод который нужно выполнить
        /// </summary>
        [SerializeField] private UnityEvent _action;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(_tag))
            {
                _action?.Invoke();
            }
        }
    }
}

