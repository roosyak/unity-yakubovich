using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class EnterCollisionComponent : MonoBehaviour
    {

        /// <summary>
        /// тег с которым срабатывает событие 
        /// </summary>
        [SerializeField] private string _tag;

        /// <summary>
        /// внешний метод который нужно выполнить по началу
        /// </summary>
        [SerializeField] private EnterEvent _action;
        /// <summary>
        /// внешний метод который нужно выполнить по завершению
        /// </summary>
        [SerializeField] private EnterEvent _actionStay;
        /// <summary>
        /// столкновение двух физических объектов 
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_tag))
                _action?.Invoke(other.gameObject);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_tag))
                _actionStay?.Invoke(other.gameObject);
        }

        // «конструкция» чтобы передать событие с параметром 
        [Serializable]
        public class EnterEvent : UnityEvent<GameObject> { 

        }
    }


}
