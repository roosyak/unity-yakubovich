using UnityEngine;
using UnityEngine.Events;

namespace PixselCrew.Components
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
        [SerializeField] private EnterEvent _action;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(_tag))
            {
                _action?.Invoke(other.gameObject);
            }
        }
    }
}

