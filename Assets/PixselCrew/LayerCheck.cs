using UnityEngine;

namespace PixselCrew
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] public LayerMask _Layer;
        [SerializeField] private bool _isTouchingLayer;
        private Collider2D _collider;

        /// <summary>
        /// соприкосается ли колайдер со слоем 
        /// </summary>
        public bool IsTouchingLayer => _isTouchingLayer;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            _isTouchingLayer = _collider.IsTouchingLayers(_Layer);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _isTouchingLayer = _collider.IsTouchingLayers(_Layer);
        }
    }

}