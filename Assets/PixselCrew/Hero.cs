using UnityEngine;
namespace PixelCrew
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _jumpSpeed = 1f;
        [SerializeField] private float _damagejumpSpeed = 1f;
        [SerializeField] private LayerCheck _groundCheck;

        [SerializeField] private float _interactionRadius;
        [SerializeField] private LayerMask _interactionLayer;

        [SerializeField] private SpawnComponent _foodSteps;
        [SerializeField] private SpawnComponent _jamp;
        [SerializeField] private SpawnComponent _jampFall;
        [SerializeField] private ParticleSystem _hitParticle;

        private Collider2D[] _interactionResult = new Collider2D[1];
        private Vector2 _direction;
        private Rigidbody2D _rigidbody;
        private int _coins = 0;
        private Animator _animator;
        private bool _isGround;
        private bool _allDoubleJump;
        private float _mimVelocityY = 0;

        private static int IsGroundKey = Animator.StringToHash("is-ground");
        private static int VerticalVilocityKey = Animator.StringToHash("vertical-vilcity");
        private static int IsRuningKey = Animator.StringToHash("is-runing");
        private static int hit = Animator.StringToHash("hit");
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
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

            if (_rigidbody.velocity.y == 0 && _mimVelocityY < (_jumpSpeed * -1.1f))
            {
                _mimVelocityY = 0;
                SpawnJampFall();
            }

            _rigidbody.velocity = new Vector2(xVilocity, yVilocity);
            _mimVelocityY = Mathf.Min(_mimVelocityY, _rigidbody.velocity.y);

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

            if (_isGround)
                _allDoubleJump = true;
            if (isJumpPressing)
                resultY = CalculateJumpVelocity(resultY);
            else if (_rigidbody.velocity.y > 0)
                resultY += 0.5f;

            return resultY;
        }

        /// <summary>
        /// пересчёт скорости по Y
        /// </summary>
        /// <returns></returns>
        private float CalculateJumpVelocity(float Y)
        {
            var isFalling = _rigidbody.velocity.y <= 0.001f;
            if (!isFalling)
            {
                return Y;
            }

            if (_isGround)
            {
                Y += _jumpSpeed;
                SpawnJamp();
            }
            else if (_allDoubleJump)
            {
                Y = _jumpSpeed;
                _allDoubleJump = false;
                SpawnJamp();
            }
            return Y;
        }
        private void UpdateSpriteDirection()
        {
            if (_direction.x > 0)
                transform.localScale = Vector3.one;
            else if (_direction.x < 0)
                // изменяем отображения в другом направлении
                transform.localScale = new Vector3(-1, 1, 1);
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

        public void AddCoin(int valCoin)
        {
            _coins += valCoin;
            Debug.Log(string.Format("монет: {0}", _coins));
        }

        public void TakeDamage()
        {
            _animator.SetTrigger(hit);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damagejumpSpeed);

            if (_coins > 0)
                SpawnCoins();
        }

        /// <summary>
        /// выкинуть монетки у героя 
        /// </summary>
        private void SpawnCoins()
        {
            var numCoinsToDispase = Mathf.Min(_coins, 5);
            _coins -= numCoinsToDispase;

            var burst = _hitParticle.emission.GetBurst(0);
            burst.count = numCoinsToDispase;
            _hitParticle.emission.SetBurst(0, burst);

            _hitParticle.gameObject.SetActive(true);
            _hitParticle.Play();
        }

        public void Interact()
        {
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _interactionRadius, _interactionResult, _interactionLayer);
            for (int i = 0; i < size; i++)
            {
                var interactable = _interactionResult[i].GetComponent<InteractableComponent>();
                if (interactable != null)
                    interactable.Interact();
            }
        }

        /// <summary>
        /// выполнить создание анимации «бега»
        /// </summary>
        public void SpawnFootDust()
        {
            _foodSteps.Spawn();
        }

        /// <summary>
        /// выполнить создание анимации «прыжка»
        /// </summary>
        public void SpawnJamp()
        {
            _jamp.Spawn();
        }
        /// <summary>
        /// выполнить создание анимации «приземления»
        /// </summary>
        public void SpawnJampFall()
        {
            _jampFall.Spawn();
        }
    }

}