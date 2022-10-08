using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] private int _frameRate = 10;
        [SerializeField] private UnityEvent<string> _onComplate;
        [SerializeField] private AnimationClip[] _clips;

        private SpriteRenderer _renderer;
        private float _secondsPreFrame;
        private int _currentClip;
        private int _currentFrame;
        private float _nexteFrameTime;
        private bool _isPlaing = true;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _secondsPreFrame = 1f / _frameRate;
            StartAnimation();
        }

        private void OnBecameVisible()
        {
            enabled = _isPlaing;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }

        public void SetClip(string clipNmae)
        {
            for (var i = 0; i < _clips.Length; i++)
                if (_clips[i].Name == clipNmae)
                {
                    _currentClip = i;
                    StartAnimation();
                    return;
                }
            enabled = _isPlaing = false;
        }
        private void StartAnimation()
        {
            _nexteFrameTime = Time.time + _secondsPreFrame;
            _currentFrame = 0;
            enabled = _isPlaing = true;
        }

        private void OnEnable()
        {
            _secondsPreFrame = 1f / _frameRate;
            _nexteFrameTime = Time.time + _secondsPreFrame;
        }

        private void Update()
        {
            if (_nexteFrameTime > Time.time || _clips.Length == 0) return;

            var clip = _clips[_currentClip];
            if (_currentFrame >= clip.Sprites.Length)
            {
                if (clip.Loop)
                    _currentFrame = 0;
                else
                {
                    enabled = _isPlaing = clip.AllowNextClip; 
                    clip.OnComplate?.Invoke();
                    _onComplate?.Invoke(clip.Name);
                    if (clip.AllowNextClip)
                    {
                        _currentFrame = 0;
                        _currentClip = (int)Mathf.Repeat(_currentClip + 1, _clips.Length);
                    }

                }
                return;

            }

            _renderer.sprite = clip.Sprites[_currentFrame];
            _nexteFrameTime += _secondsPreFrame;
            _currentFrame++;
        }
    }

    [Serializable]
    public class AnimationClip
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private bool _loop;
        [SerializeField] private bool _allowNextClip;
        [SerializeField] private UnityEvent _onComplate;

        public string Name => _name;
        public Sprite[] Sprites => _sprites;
        public bool Loop => _loop;
        public bool AllowNextClip => _allowNextClip;
        public UnityEvent OnComplate => _onComplate;
    }
}