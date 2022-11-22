using UnityEngine;
using PixselCrew.Utils;
using System;

namespace PixselCrew.Creatures
{
    public class SeashellTrapAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;

        [Header("Melle")]
        [SerializeField] private Cooldown _meleeCoolDown;
        [SerializeField] private CheckCircleOverlap _meleeAttack;
        [SerializeField] private LayerCheck _meleeCanAttack;

        [Header("Range")]
        [SerializeField] private Cooldown _rangeCoolDown;
        [SerializeField] private SpawnComponent _rangeAtack;


        private int Melee = Animator.StringToHash("melee");
        private int Range = Animator.StringToHash("range");
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        private void Update()
        {
            if (_vision.IsTouchingLayer)
            {
                if (_meleeCanAttack.IsTouchingLayer)
                {
                    if (_meleeCoolDown.IsReady)
                        MelleAttack();
                    return;
                }

                if (_rangeCoolDown.IsReady)
                    RangeAttack();
            }
        }

        private void RangeAttack()
        {
            _rangeCoolDown.Reset();
            _animator.SetTrigger(Range);
        }

        private void MelleAttack()
        {
            _meleeCoolDown.Reset();
            _animator.SetTrigger(Melee);
        }

        public void OnMeleeAttack()
        {
            _meleeAttack.Check();
        }

        public void OnRangeAttack()
        {
            _rangeAtack.Spawn();
        }
    }
}