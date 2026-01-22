using System;
using PanzerHero.Runtime.Units.Abstract.Base;

namespace PanzerHero.Runtime.Units.Components
{
    public class UnitAI : BaseRigComponent<UnitRig>
    {
        UnitAttacker attacker;
        UnitMovement movement;
        UnitTargetCalculator targetCalculator;

        public override void Initialize()
        {
            base.Initialize();
            
            movement = GetComponent<UnitMovement>();
            targetCalculator = GetComponent<UnitTargetCalculator>();
            attacker = GetComponent<UnitAttacker>();
        }

        private void Update()
        {
            UpdateAI();
        }

        void UpdateAI()
        {
            if (targetCalculator.HasTarget())
            {
                if (targetCalculator.IsTargetOutOfVision())
                {
                    return;
                }

                var target = targetCalculator.GetTarget();
                if (attacker.IsTargetInFireRange())
                {
                    movement.Stop();

                    var targetPoint = target.Position;
                    attacker.TryFire(targetPoint);
                }
                else
                {
                    var position = target.Position;
                    movement.SetDestination(position);
                }
            }
            else
            {
                movement.Stop();
            }
        }
    }
}