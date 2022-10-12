using UnityEditor.Animations;
using UnityEngine;
namespace PixelCrew
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _jumpSpeed = 1f;
        [SerializeField] private float _damagejumpSpeed = 1f;
        [SerializeField] private float _slamDownVilocity;
        [SerializeField] private int _damage; // сила урона героя 
        [SerializeField] private LayerCheck _groundCheck;

        [SerializeField] private float _interactionRadius;
        [SerializeField] private LayerMask _interactionLayer;

        [SerializeField] private CheckCircleOverlap _attackRange;

        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        [Space]
        [Header("Particles")]
        [SerializeField] private SpawnComponent _foodSteps; // анимация бега 
        [SerializeField] private SpawnComponent _jamp;      // анимация начала прыжка 
        [SerializeField] private SpawnComponent _jampFall;  // анимация падения 
        [SerializeField] private SpawnComponent _attack;  // анимация атаки меча 
        [SerializeField] private ParticleSystem _hitParticle;

        private Collider2D[] _interactionResult = new Collider2D[1];


        private Vector2 _direction;
        private Rigidbody2D _rigidbody;
        private GameSession _session;

        private Animator _animator;
        private bool _isGround;
        private bool _allDoubleJump;

        private static int IsGroundKey = Animator.StringToHash("is-ground");
        private static int VerticalVilocityKey = Animator.StringToHash("vertical-vilcity");
        private static int IsRuningKey = Animator.StringToHash("is-runing");
        private static int hit = Animator.StringToHash("hit");
        private static int AttackKey = Animator.StringToHash("attack");

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var heals = GetComponent<HealthComponent>();
            heals.SetHealth(_session.Data.Hp);
            UpdateHeroWeapon();
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp = currentHealth;
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
                _jamp.Spawn();
            }
            else if (_allDoubleJump)
            {
                Y = _jumpSpeed;
                _allDoubleJump = false;
                _jamp.Spawn();
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
            _session.Data.Coins += valCoin;
            Debug.Log(string.Format("монет: {0}", _session.Data.Coins));
        }

        public void TakeDamage()
        {
            _animator.SetTrigger(hit);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damagejumpSpeed);

            if (_session.Data.Coins > 0)
                SpawnCoins();
        }

        /// <summary>
        /// выкинуть монетки у героя 
        /// </summary>
        private void SpawnCoins()
        {
            var numCoinsToDispase = Mathf.Min(_session.Data.Coins, 5);
            _session.Data.Coins -= numCoinsToDispase;

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

        private void OnCollisionEnter2D(Collision2D other)
        {
            // проверяем пересечением со слоем 
            if (other.gameObject.IsInLayer(_groundCheck._groundLayer))
            {
                var contact = other.contacts[0];
                // проверяем силу соприкосновения 
                if (contact.relativeVelocity.y >= _slamDownVilocity)
                {
                    _jampFall.Spawn();
                }
            }
        }

        public void Attack()
        {
            if (!_session.Data.IsArmed)
                return;
            _animator.SetTrigger(AttackKey);
            _attack.Spawn();
        }

        public void onDoAttack()
        {
            var gos = _attackRange.GetObjectsInRange();
            foreach (var go in gos)
            {
                var hp = go.GetComponent<HealthComponent>();
                if (hp != null && go.CompareTag("Enemy"))
                {
                    hp.ApplyDamage(-_damage);
                }
            }
        }

        public void ArmHero()
        {
            _session.Data.IsArmed = true;
            UpdateHeroWeapon();
        }

        private void UpdateHeroWeapon()
        {
            _animator.runtimeAnimatorController = _session.Data.IsArmed ? _armed : _disarmed;

        }
    }

}