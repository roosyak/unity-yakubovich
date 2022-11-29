using UnityEditor;
using UnityEngine;
using PixselCrew.Components;

namespace PixselCrew.Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private bool _invertScale;
        [SerializeField] private float _speed = 1f;
        [SerializeField] protected float _jumpSpeed = 1f;
        [SerializeField] private float _damageVelocity = 1f;
        [SerializeField] private int _damage; // сила урона героя 
        [SerializeField] protected LayerCheck _groundCheck;

        [Header("Checkers")]
        [SerializeField] private CheckCircleOverlap _attackRange;

        [SerializeField] protected PixselCrew.Components.SpawnListComponent _particles;


        private Vector2 _direction;
        protected Rigidbody2D Rigidbody;
        protected Animator Animator;
        protected PlaySoudsComponent Sounds;
        protected bool IsGrounded;
        private bool _isJumping;


        private static int IsGroundKey = Animator.StringToHash("is-ground");
        private static int VerticalVilocityKey = Animator.StringToHash("vertical-vilcity");
        private static int IsRuningKey = Animator.StringToHash("is-runing");
        private static int hit = Animator.StringToHash("hit");
        private static int AttackKey = Animator.StringToHash("attack");

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            Sounds = GetComponent<PlaySoudsComponent>();
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        protected virtual void Update()
        {
            IsGrounded = _groundCheck.IsTouchingLayer;
        }

        private void FixedUpdate()
        {
            var xVilocity = _direction.x * _speed;
            var yVilocity = CalculateYVilocity();

            Rigidbody.velocity = new Vector2(xVilocity, yVilocity);

            Animator.SetBool(IsGroundKey, IsGrounded);
            Animator.SetFloat(VerticalVilocityKey, Rigidbody.velocity.y);
            Animator.SetBool(IsRuningKey, _direction.x != 0);

            UpdateSpriteDirection(_direction);
        }

        /// <summary>
        /// пересчёт Y координаты 
        /// </summary>
        /// <returns></returns>
        protected virtual float CalculateYVilocity()
        {
            var resultY = Rigidbody.velocity.y;

            var isJumpPressing = _direction.y > 0;

            if (isJumpPressing)
            {
                var isFalling = Rigidbody.velocity.y <= 0.001f;
                resultY = isFalling ? CalculateJumpVelocity(resultY) : resultY;
            }
            else if (Rigidbody.velocity.y > 0)
                resultY += 0.5f;

            return resultY;
        }

        /// <summary>
        /// пересчёт скорости по Y
        /// </summary>
        /// <returns></returns>
        protected virtual float CalculateJumpVelocity(float Y)
        {
            if (IsGrounded)
            {
                Y = _jumpSpeed;
                DoJumpVfx();
            }
            return Y;
        }

        protected void DoJumpVfx()
        {
            _particles.Spawn("Jump");
            Sounds.Play("Jump");
        }

        public void UpdateSpriteDirection(Vector2 direction)
        {
            var multiplay = _invertScale ? -1 : 1;
            if (direction.x > 0)
                transform.localScale = new Vector3(multiplay, 1, 1);
            else if (direction.x < 0)
                // изменяем отображения в другом направлении
                transform.localScale = new Vector3(-1 * multiplay, 1, 1);
        }

        public virtual void TakeDamage()
        {
            Animator.SetTrigger(hit);
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageVelocity);
        }

        public virtual void Attack()
        {
            Animator.SetTrigger(AttackKey);
            Sounds.Play("Melee");
            // _particles.Spawn("Attack");
            //_attack.Spawn();
        }
        public void onDoAttack()
        {
            _attackRange.Check();
            /*var gos = _attackRange.GetObjectsInRange();
            foreach (var go in gos)
            {
                var hp = go.GetComponent<HealthComponent>();
                if (hp != null && go.CompareTag("Enemy"))
                {
                    hp.ApplyDamage(-_damage);
                }
            }*/
        }
    }
}