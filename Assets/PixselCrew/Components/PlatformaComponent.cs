
using UnityEngine;

namespace PixelCrew.Components
{
    /// <summary>
    /// движущийся элемент уровня
    /// </summary>
    public class PlatformaComponent : MonoBehaviour
    {
        /// <summary>
        /// объект которые будем двигать
        /// </summary>
        [SerializeField] private GameObject _platforma;

        /// <summary>
        /// скорость движения
        /// </summary>
        [SerializeField] private float _speed = 0.01f;

        /// <summary>
        /// ось перемещения горизонтальная/вертикальная
        /// </summary>
        [SerializeField] private bool _isVertical = false;
        /// <summary>
        /// направление движения назад (вниз)
        /// </summary>
        [SerializeField] private bool _back = false;

        /// <summary>
        /// текущая скорость и направление
        /// </summary>
        private float _currentSpeed;

        private void Awake()
        {
            _currentSpeed = _back ? (_speed * -1) : _speed;
        }
        /// <summary>
        /// изменить направление движения
        /// </summary>
        public void ChangeDirectionMovement()
        {
            Debug.Log("ChangeDirectionMovement...");
            _back = !_back;
            _currentSpeed = _back ? (_speed * -1) : _speed;
        }

        private void FixedUpdate()
        {
            if (_isVertical)
                transform.position = new Vector3(transform.position.x, transform.position.y + _currentSpeed, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x + _currentSpeed, transform.position.y, transform.position.z);

        }
    }
}
