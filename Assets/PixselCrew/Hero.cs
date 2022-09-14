using UnityEngine;
namespace PixelCrew
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _jumpSpeed = 1f;
        [SerializeField] private LayerCheck _groundCheck;

        private Vector2 _direction;
        private Rigidbody2D _rigidbody;
        private float _coins = 0f;
        private Animator _animator;
        private SpriteRenderer _sprite;

        private static int IsGroundKey = Animator.StringToHash("is-ground");
        private static int VerticalVilocityKey = Animator.StringToHash("vertical-vilcity");
        private static int IsRuningKey = Animator.StringToHash("is-runing");
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>();
        }
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);
            var _isJumping = _direction.y > 0;
            var isGround = IsGrounded();
            if (_isJumping)
            {
                if (isGround && _rigidbody.velocity.y <= 0)
                {
                    _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
                }
            }
            else if (_rigidbody.velocity.y > 0)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
            }
            _animator.SetBool(IsGroundKey, isGround);
            _animator.SetFloat(VerticalVilocityKey, _rigidbody.velocity.y);
            _animator.SetBool(IsRuningKey, _direction.x != 0);

            UpdateSpriteDirection();
        }


        private void UpdateSpriteDirection()
        {
            if (_direction.x > 0)
            {
                _sprite.flipX = false;
            }
            else if (_direction.x < 0)
            {
                _sprite.flipX = true;
            }
        }
        /// <summary>
        /// стоит ли герой на поверхности  
        /// </summary>
        /// <returns></returns>
        private bool IsGrounded()
        {
            return _groundCheck.IsTouchingLayer;
        }

        public void SaySomething()
        {
            Debug.Log("SaySomething");
        }

        public void AddCoin(float valCoin)
        {
            _coins += valCoin;
            Debug.Log(string.Format("монет: {0}", _coins));
        }
    }

}