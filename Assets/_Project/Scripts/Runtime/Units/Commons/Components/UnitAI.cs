using System;
using PanzerHero.Runtime.Units.Abstract.Base;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Components
{
    public class UnitAI : BaseRigComponent<UnitRig>
    {
        [Serializable]
        public enum EUnitState
        {
            Idle,
            Patrol,
            Chase,
            Attack
        }
        
        [SerializeField] EUnitState state;
        
        UnitAttacker attacker;
        UnitMovement movement;
        UnitTargetCalculator targetCalculator;

        Vector3 startPosition;
        
        public event Action<EUnitState> OnStateChange;

        public override void Initialize()
        {
            base.Initialize();
            
            movement = GetComponent<UnitMovement>();
            targetCalculator = GetComponent<UnitTargetCalculator>();
            attacker = GetComponent<UnitAttacker>();

            startPosition = movement.GetCurrentPosition();
        }
        
        public void SetState(EUnitState state)
        {
            this.state = state;
            OnStateChange?.Invoke(state);
        }
        
        private void Update()
        {
            UpdateAI();
        }

        void UpdateAI()
        {
            var target = targetCalculator.GetTarget();
            var targetPoint = target?.Position ?? new Vector3();
            
            if (targetCalculator.HasTarget() && !targetCalculator.IsTargetOutOfVision())
            {
                if (attacker.IsTargetInFireRange())
                {
                    SetState(EUnitState.Attack);
                    attacker.TryFire(targetPoint);
                }
                else
                {
                    SetState(EUnitState.Chase);
                    movement.SetDestination(targetPoint);
                }
            }
            else
            {
                if (IsOnStartPoint())
                {
                    SetState(EUnitState.Idle);
                }
                else
                {
                    SetState(EUnitState.Patrol);
                }
            }
            
            UpdateMovement();
        }

        void UpdateMovement()
        {
            switch (state)
            {
                case EUnitState.Idle:
                    movement.Stop();
                    break;
                case EUnitState.Patrol:
                    movement.SetDestination(startPosition);
                    break;
                case EUnitState.Chase:
                    var target = targetCalculator.GetTarget();
                    movement.SetDestination(target?.Position ?? movement.GetCurrentPosition());
                    break;
                case EUnitState.Attack:
                    movement.Stop();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        bool IsOnStartPoint()
        {
            return (movement.GetCurrentPosition() - startPosition).sqrMagnitude < 0.25f;
        }
    }
}