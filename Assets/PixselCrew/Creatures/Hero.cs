using UnityEditor.Animations;
using UnityEngine;
namespace PixselCrew.Creatures
{
    public class Hero : Creature
    {
        [SerializeField] private CheckCircleOverlap _interactionCheck;
       //  [SerializeField] private LayerMask _interactionLayer;

        [SerializeField] private float _slamDownVilocity;
        [SerializeField] private float _interactionRadius;

        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        [Space]
        [Header("Particles")]
        [SerializeField] private ParticleSystem _hitParticle;

        private static int ThrowKey = Animator.StringToHash("throw");

        // private Collider2D[] _interactionResult = new Collider2D[1];

        private GameSession _session;

        private bool _allDoubleJump;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var heals = GetComponent<HealthComponent>();
            heals.SetHealth(_session.Data.Hp);
            UpdateHeroWeapon();
        }

        public void Throw()
        {
            Animator.SetTrigger(ThrowKey);
        }

        public void OnDoThrow()
        {
            _particles.Spawn("Throw");
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp = currentHealth;
        }


        protected override void Update()
        {
            base.Update();
        }



        /// <summary>
        /// пересчёт Y координаты 
        /// </summary>
        /// <returns></returns>
        protected override float CalculateYVilocity()
        {
            //var resultY = _rigidbody.velocity.y;
            //var isJumpPressing = _direction.y > 0;

            if (IsGrounded)
                _allDoubleJump = true;

            // else if (_rigidbody.velocity.y > 0)
            //   resultY += 0.5f;

            return base.CalculateYVilocity();
        }

        /// <summary>
        /// пересчёт скорости по Y
        /// </summary>
        /// <returns></returns>
        protected override float CalculateJumpVelocity(float Y)
        {
            if (!IsGrounded && _allDoubleJump)
            {
                _particles.Spawn("Jump");
                // Y = _jumpSpeed;
                _allDoubleJump = false;
                return _jumpSpeed;
            }
            return base.CalculateJumpVelocity(Y);
        }


        public void AddCoin(int valCoin)
        {
            _session.Data.Coins += valCoin;
            Debug.Log(string.Format("монет: {0}", _session.Data.Coins));
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
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
            _interactionCheck.Check();
           /* var size = Physics2D.OverlapCircleNonAlloc(transform.position, _interactionRadius, _interactionResult, _interactionLayer);
            for (int i = 0; i < size; i++)
            {
                var interactable = _interactionResult[i].GetComponent<InteractableComponent>();
                if (interactable != null)
                    interactable.Interact();
            }*/
        }

        /// <summary>
        /// выполнить создание анимации «бега»
        /// </summary>
       /* public void SpawnFootDust()
        {
            _particles.Spawn("Steps");
            //_foodSteps.Spawn();
        }*/

        private void OnCollisionEnter2D(Collision2D other)
        {
            // проверяем пересечением со слоем 
            if (other.gameObject.IsInLayer(_groundCheck._Layer))
            {
                var contact = other.contacts[0];
                // проверяем силу соприкосновения 
                if (contact.relativeVelocity.y >= _slamDownVilocity)
                {
                    _particles.Spawn("SlamDown");
                    //_jampFall.Spawn();
                }
            }
        }

        public override void Attack()
        {
            if (!_session.Data.IsArmed)
                return;
            base.Attack();
        }



        public void ArmHero()
        {
            _session.Data.IsArmed = true;
            UpdateHeroWeapon();
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = _session.Data.IsArmed ? _armed : _disarmed;

        }
    }

}