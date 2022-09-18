using UnityEngine;
namespace PixelCrew
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _jumpSpeed = 1f;
        [SerializeField] private float _damagejumpSpeed = 1f;
        [SerializeField] private LayerCheck _groundCheck;

        private Vector2 _direction;
        private Rigidbody2D _rigidbody;
        private float _coins = 0f;
        private Animator _animator;
        private SpriteRenderer _sprite;
        private bool _isGround;
        private bool _allDoubleJump;

        private static int IsGroundKey = Animator.StringToHash("is-ground");
        private static int VerticalVilocityKey = Animator.StringToHash("vertical-vilcity");
        private static int IsRuningKey = Animator.StringToHash("is-runing");
        private static int hit = Animator.StringToHash("hit");
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

        private void Update()
        {
            _isGround = IsGrounded();
        }

        private void FixedUpdate()
        {
            var xVilocity = _direction.x * _speed;
            var yVilocity = CalculateYVilocity();

            _rigidbody.velocity = new Vector2(xVilocity, yVilocity);

            _animator.SetBool(IsGroundKey, _isGround);
            _animator.SetFloat(VerticalVilocityKey, _rigidbody.velocity.y);
            _animator.SetBool(IsRuningKey, _direction.x != 0);

            UpdateSpriteDirection();
        }

        /// <summary>
        /// пересчёт Y координаты 
        /// </summary>
        /// <returns></returns>
        private float CalculateYVilocity()
        {
            var resultY = _rigidbody.velocity.y;

            var isJumpPressing = _direction.y > 0;

            if (_isGround) _allDoubleJump = true;
            if (isJumpPressing)
            {
                resultY = CalculateJumpVelocity(resultY);
            }
            else if (_rigidbody.velocity.y > 0)
            {
                resultY += 0.5f;
            }

            return resultY;
        }

        /// <summary>
        /// пересчёт скорости по Y
        /// </summary>
        /// <returns></returns>
        private float CalculateJumpVelocity(float Y)
        {
            var isFalling = _rigidbody.velocity.y <= 0.001f;
            if (!isFalling) return Y;

            if (_isGround)
            {
                Y += _jumpSpeed;
            }
            else if (_allDoubleJump) {
                Y = _jumpSpeed;
                _allDoubleJump = false;
            }
            return Y;
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

        public void TakeDamage() {
            _animator.SetTrigger(hit);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damagejumpSpeed);
        }
    }

}