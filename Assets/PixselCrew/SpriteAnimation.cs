using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] private int _frameRate = 10;
        [SerializeField] private bool _loop = true;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private UnityEvent _onComplate;

        private SpriteRenderer _renderer;
        private float _secondsPreFrame;
        private int _currentSpriteIndex;
        private float _nexteFrameTime;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _secondsPreFrame = 1f / _frameRate;
            _nexteFrameTime = Time.time + _secondsPreFrame;
            _currentSpriteIndex = 0;
        }

        private void Update()
        {
            if (_nexteFrameTime > Time.time) return;

            if (_currentSpriteIndex >= _sprites.Length)
            {
                if (_loop)
                {
                    _currentSpriteIndex = 0;
                }
                else
                {
                    enabled = false;
                    _onComplate?.Invoke();
                    return;
                }
            }

            _renderer.sprite = _sprites[_currentSpriteIndex];
            _nexteFrameTime += _secondsPreFrame;
            _currentSpriteIndex++;
        }
    }
}