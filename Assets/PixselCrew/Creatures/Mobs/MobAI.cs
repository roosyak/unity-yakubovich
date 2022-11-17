using System;
using System.Collections;
using UnityEngine;

namespace PixselCrew.Creatures
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;

        [SerializeField] private float _alarmDelay = 0.5f;
        [SerializeField] private float _attacCoolDown = 1f;
        [SerializeField] private float _missCoolDown = 0.5f;

        private Coroutine _current;
        private GameObject _target;
        private Creature _creature;
        private Animator _animator;


        private int isDeadKey = Animator.StringToHash("is-dead");

        private PixselCrew.Components.SpawnListComponent _particles;
        private bool _isDead;
        private Patrol _patrol;

        private void Awake()
        {
            _particles = GetComponent<PixselCrew.Components.SpawnListComponent>();
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }

        public void OnHeroInVision(GameObject go)
        {
            if (_isDead)
                return;
            _target = go;
            StartState(ArgoToHero());
        }

        private IEnumerator ArgoToHero()
        {
            LookAtHero();
            _particles.Spawn("Excamination");
            yield return new WaitForSeconds(_alarmDelay);
            StartState(GoToHero());
        }

        private void LookAtHero()
        {
            _creature.SetDirection(Vector2.zero);
            var direction = GetDirectionTotarget();
            _creature.UpdateSpriteDirection(direction);
        }

        private IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                    SetDirectionToTarget();
                yield return null;
            }

            _creature.SetDirection(Vector2.zero);
            _particles.Spawn("MissHero");
            yield return new WaitForSeconds(_missCoolDown);

            StartState(_patrol.DoPatrol());

        }

        private IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attacCoolDown);
            }
            StartState(GoToHero());
        }

        private void SetDirectionToTarget()
        {
            var direction = GetDirectionTotarget();
            _creature.SetDirection(direction);
        }

        private Vector2 GetDirectionTotarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0f;
            return direction.normalized;
        }
        private void StartState(IEnumerator coroutine)
        {
            _creature.SetDirection(Vector2.zero);

            if (_current != null)
                StopCoroutine(_current);
            _current = StartCoroutine(coroutine);
        }

        public void OnDie()
        {
            if (_isDead)
                return;
            Debug.Log("AI die");
            _isDead = true;
            _animator.SetBool(isDeadKey, true);
            _creature.SetDirection(Vector2.zero);
            if (_current != null)
                StopCoroutine(_current);

        }
    }
}