using System;
using System.Collections.Generic;
using GameDevUtils.Runtime;
using GameDevUtils.Runtime.Extensions;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Abstract.Base
{
    public abstract class BaseTargetCalculator<T> : BaseRigComponent<T>, ISystemComponent, IManagedComponent
        where T : BaseRig
    {
        public bool IsDisabled => Rig.IsDisabled;

        protected abstract float Radius { get; }
        protected abstract LayerMask TargetLayer { get; }
        
        [Header("--DEBUG--")]
        [SerializeReference] private BaseRig target;
        [SerializeReference] private List<BaseRig> unitsList = new List<BaseRig>();

        public override void Initialize()
        {
            base.Initialize();
            InstantiateCollider();
        }

        void InstantiateCollider()
        {
            var rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            
            var col = gameObject.AddComponent<SphereCollider>();
            col.radius = Radius;
            col.isTrigger = true;
        }

        public void UpdateTarget()
        {
            if (unitsList.Count == 0)
            {
                target = null;
                return;
            }

            if (unitsList.Count == 1)
            {
                target = unitsList[0];
                return;
            }
            
            var countAtStart = unitsList.Count;

            BaseRig closestTarget = null;
            float closestDistance = float.MaxValue;

            for (int i = 0; i < unitsList.Count; i++)
            {
                if (unitsList.Count != countAtStart)
                {
                    return;
                }
                
                var unit = unitsList[i];
                if (!IsVerifiedUnit(unit))
                {
                    countAtStart--;
                    unitsList.RemoveAt(i);
                    
                    continue;
                }
                
                var distance = Vector3.Distance(unit.Position, Rig.Position);
                
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

        bool IsVerifiedUnit(IUnit unit)
        {
            if (!unit.IsAlive)
            {
                return false;
            }

            return true;
        }
        
        public bool HasTarget() => target;

        public IUnit GetTarget() => target;

        public bool IsTargetOutOfVision()
        {
            return IsTargetOutOfRange(Radius + 0.1f);
        }

        public bool IsTargetInRange(float maxDistance)
        {
            if (!target)
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
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.IsSameMask(TargetLayer))
            {
                var iunit = other.GetComponent<BaseRig>();
                unitsList.Add(iunit);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.IsSameMask(TargetLayer))
            {
                var iunit = other.GetComponent<BaseRig>();
                unitsList.Remove(iunit);
            }
        }
    }
}