using UnityEditor.Animations;
using UnityEngine;
using PixselCrew.Model;
using PixselCrew.Utils;
using System;

namespace PixselCrew.Creatures
{
    public class Hero : Creature, ICanAddInInventory
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
        [SerializeField] private SpawnComponent _throwSpawner;

        private static int ThrowKey = Animator.StringToHash("throw");

        // private Collider2D[] _interactionResult = new Collider2D[1];

        private GameSession _session;
        private HealthComponent _health;


        private bool _allDoubleJump;

        private const string idCoin = "Coin";
        private const string SwordId = "Sword";

        private int CoinsCount => _session.Data.Inventory.Count(idCoin);
        private int SwordCount => _session.Data.Inventory.Count(SwordId);

        private string SelectedItemId => _session.QuickInventory.SelectedItem.Id;
        private bool CanThrow
        {
            get
            {
                // если мечь последний, не можем кидаться 
                if (SelectedItemId == SwordId)
                    return SwordCount > 1;

                // выбранный предмет которым можно кидаться 
                var def = DefsFacade.I.Items.Get(SelectedItemId);
                return def.HasTag(ItemTag.Throwable);
            }
        }

        protected override void Awake()
        {
            base.Awake();
        }

        /*public void UsePotion()
        {
            var potionCount = _session.Data.Inventory.Count("HealthPotion");
            if (potionCount > 0)
            {
                _session.Data.Hp.Value += 7;
                _session.Data.Inventory.Remove("HealthPotion", 1);
            }
        }*/

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _health = GetComponent<HealthComponent>();

            // добавляем подписчика 
            _session.Data.Inventory.OnChanged += OnInventoryChanged;
            _health.SetHealth(_session.Data.Hp.Value);
            UpdateHeroWeapon();
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == SwordId)
                UpdateHeroWeapon();
        }
        public void UseInventory()
        {
            // для текущего выбранного предмета 

            // если выбранный предмет можно бросить 
            if (IsSelectedTag(ItemTag.Throwable))
                PerformThrowing();
            // если можно применить зелье 
            else if (IsSelectedTag(ItemTag.Potion))
                UsePotion();

        }

        private void UsePotion()
        {
            var potion = DefsFacade.I.Potions.Get(SelectedItemId);

            switch (potion.Effect)
            {
                case Effect.AddHp:
                    _session.Data.Hp.Value += (int)potion.Value;
                    break;

                case Effect.SpeedUp:
                    _speedUpCoolDown.Value = _speedUpCoolDown.TimeLasts + potion.Time;
                    _additionalSpeed = Mathf.Max(potion.Value, _additionalSpeed);
                    _speedUpCoolDown.Reset();
                    break;
            }

            _session.Data.Inventory.Remove(potion.Id, 1);
        }

        private readonly Cooldown _speedUpCoolDown = new Cooldown();
        private float _additionalSpeed;

        protected override float ClaculateSpeed()
        {
            if (_speedUpCoolDown.IsReady)
                _additionalSpeed = 0f;
            return base.ClaculateSpeed() + _additionalSpeed;
        }

        private bool IsSelectedTag(ItemTag tag)
        {
            return _session.QuickInventory.SelectedDef.HasTag(tag);
        }

        private void PerformThrowing()
        {

            if (_throwCoolDown.IsReady && CanThrow)
            {
                Animator.SetTrigger(ThrowKey);
                Sounds.Play("Sword");
                _throwCoolDown.Reset();
            }
        }

        public void OnDoThrow()
        {
            Sounds.Play("Range");

            // текущий выбранный предмет 
            var throwableId = _session.QuickInventory.SelectedItem.Id;
            // можно ли его кинуть 
            var throwableDef = DefsFacade.I.Throwable.Get(throwableId);
            _throwSpawner.SetPrefab(throwableDef.Projectile);
            _throwSpawner.Spawn();
            _session.Data.Inventory.Remove(throwableId, 1);
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp.Value = currentHealth;
        }


        protected override void Update()
        {
            base.Update();
        }



        /// <summary>
        /// пересчёт Y координаты 
        /// </summary>
        /// <returns></returns>
        protected override float CalculateYVelocity()
        {
            //var resultY = _rigidbody.velocity.y;
            //var isJumpPressing = _direction.y > 0;

            if (IsGrounded)
                _allDoubleJump = true;

            // else if (_rigidbody.velocity.y > 0)
            //   resultY += 0.5f;

            return base.CalculateYVelocity();
        }

        /// <summary>
        /// пересчёт скорости по Y
        /// </summary>
        /// <returns></returns>
        protected override float CalculateJumpVelocity(float Y)
        {
            if (!IsGrounded && _allDoubleJump)
            {
                _allDoubleJump = false;
                DoJumpVfx();
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
            _particles.Spawn("Attack");
            //_attack.Spawn();
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

            _session.Data.Inventory.Add(SwordId, 1);
            //Debug.Log(string.Format("Arms: {0}", _session.Data.Arms));
        }


        private void UpdateHeroWeapon()
        {

            Animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _disarmed;

        }
        // слудующий лемент выстрого инвенторя 
        public void NextItem()
        {
            _session.QuickInventory.SetNextItem();
        }
    }

}