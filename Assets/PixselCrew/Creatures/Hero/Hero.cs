using UnityEditor.Animations;
using UnityEngine;
using PixselCrew.Model;
using PixselCrew.Utils;
namespace PixselCrew.Creatures
{
    public class Hero : Creature
    {
        [SerializeField] private CheckCircleOverlap _interactionCheck;
        //  [SerializeField] private LayerMask _interactionLayer;

        [SerializeField] private float _slamDownVilocity;
        [SerializeField] private float _interactionRadius;

        [SerializeField] private Cooldown _throwCoolDown;
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        [Space]
        [Header("Particles")]
        [SerializeField] private ParticleSystem _hitParticle;

        private static int ThrowKey = Animator.StringToHash("throw");

        // private Collider2D[] _interactionResult = new Collider2D[1];

        private GameSession _session;

        private bool _allDoubleJump;

        private const string idCoin = "Coin";
        private const string idSword = "Sword";

        private int CoinsCount => _session.Data.Inventory.Count(idCoin);
        private int SwordCount => _session.Data.Inventory.Count(idSword);

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var heals = GetComponent<HealthComponent>();

            // добавляем подписчика 
            _session.Data.Inventory.OnChanged += OnInventoryChanged;
            heals.SetHealth(_session.Data.Hp);
            UpdateHeroWeapon();
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == idSword)
                UpdateHeroWeapon();
        }
        public void Throw()
        {
            if (_throwCoolDown.IsReady)
                if (ArmDec())
                {
                    Animator.SetTrigger(ThrowKey);
                    _throwCoolDown.Reset();
                }
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

        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.Add(id, value);
        }

        /* public void AddCoin(int valCoin)
         {
             _session.Data.Coins += valCoin;
             //Debug.Log(string.Format("монет: {0}", _session.Data.Coins));
         }*/

        public override void TakeDamage()
        {
            base.TakeDamage();
            if (CoinsCount > 0)
                SpawnCoins();
        }

        /// <summary>
        /// выкинуть монетки у героя 
        /// </summary>
        private void SpawnCoins()
        {
            var numCoinsToDispase = Mathf.Min(CoinsCount, 5);

            _session.Data.Inventory.Remove(idCoin, numCoinsToDispase);

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
            if (SwordCount <= 0)
                return;
            base.Attack();
        }



        /*public void ArmHero()
        {
            ArmInc();
            _session.Data.IsArmed = true;
            UpdateHeroWeapon();
        }*/

        private void ArmInc()
        {
            //_session.Data.Arms++;

            _session.Data.Inventory.Add(idSword, 1);
            //Debug.Log(string.Format("Arms: {0}", _session.Data.Arms));
        }

        private bool ArmDec()
        {
            //if (_session.Data.Arms > 1)
            if (SwordCount > 1)
            {
                // _session.Data.Arms--;

                _session.Data.Inventory.Remove(idSword, 1);
                //Debug.Log(string.Format("Arms: {0}", _session.Data.Arms));
                return true;
            }
            return false;
        }

        private void UpdateHeroWeapon()
        {

            Animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _disarmed;

        }
    }

}