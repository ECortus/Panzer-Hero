using System;
using System.Collections.Generic;
using GameDevUtils.Runtime;
using GameDevUtils.Runtime.Extensions;
using Plugins.GameDevUtils.Runtime.Extensions;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Abstract.Base
{
    public abstract class BaseTargetCalculator<T> : BaseRigComponent<T>, ISystemComponent, IManagedComponent
        where T : BaseRig
    {
        public bool IsDisabled => !Rig || Rig.IsDisabled;

        protected abstract float Radius { get; }
        protected abstract LayerMask TargetLayer { get; }
        
        IUnit target;
        UnitsManager unitsManager;

        public override void Initialize()
        {
            base.Initialize();
            unitsManager = UnitsManager.GetInstance;
        }

        public void UpdateTarget()
        {
            var units = GetListOfUnits();

            IUnit closestTarget = null;
            float closestDistance = float.MaxValue;

            for (int i = 0; i < units.Count; i++)
            {
                var unit = units[i];
                if (!IsVerifiedUnit(unit))
                {
                    continue;
                }
                
                var distance = Vector3.Distance(unit.Position, Rig.Position);

                if (distance > Radius)
                {
                    continue;
                }
                
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = unit;
                }
            }

            target = closestTarget;
        }

        public void ResetTarget()
        {
            target = null;
        }

        List<IUnit> GetListOfUnits()
        {
            List<IUnit> units;
            
            var faction = Rig.Faction;
            if (faction == EUnitFaction.Player)
            {
                units = unitsManager.EnemyFaction;
            }
            else if (faction == EUnitFaction.Enemy)
            {
                units = unitsManager.PlayerFaction;
            }
            else
            {
                throw new NotImplementedException();
            }

            return units;
        }

        bool IsVerifiedUnit(IUnit unit)
        {
            if (!unit.IsAlive)
            {
                return false;
            }

            return true;
        }
        
        public bool HasTarget() => target != null;

        public IUnit GetTarget()
        {
            if (IsVerifiedUnit(target))
            {
                return target;
            }

            return null;
        }

        public bool IsTargetOutOfVision()
        {
            return IsTargetOutOfRange(Radius + 0.1f);
        }

        public bool IsTargetInRange(float maxDistance)
        {
            if (target == null)
            {
                return false;
            }
            
            var distance = (target.Position - Rig.Position).sqrMagnitude;
            return distance <= maxDistance * maxDistance;
        }
        
        public bool IsTargetOutOfRange(float maxDistance)
        {
            return !IsTargetInRange(maxDistance);
        }

        bool TryGetRigFromCollider(Collider other, out BaseRig rig)
        {
            var baseRig = other.GetComponent<BaseRig>();
            if (!baseRig)
            {
                baseRig = other.GetComponentInParent<BaseRig>();
                if (!baseRig)
                {
                    rig = null;
                    return false;
                }
            }

            rig = baseRig;
            return true;
        }
    }
}