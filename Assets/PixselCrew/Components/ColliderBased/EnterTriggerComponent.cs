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
        /// слой с которым срабатывает событие 
        /// </summary>
        [SerializeField] private LayerMask _layer = ~0; // по умолчанию «всё включено»

        /// <summary>
        /// внешний метод который нужно выполнить
        /// </summary>
        [SerializeField] private EnterEvent _action;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.IsInLayer(_layer))
                return;

            if (!string.IsNullOrEmpty(_tag) && !other.gameObject.CompareTag(_tag))
                return; 
            
                _action?.Invoke(other.gameObject);
            
        }

        public void SetTag(string tag) {
            _tag = tag;
        }
    }
}

