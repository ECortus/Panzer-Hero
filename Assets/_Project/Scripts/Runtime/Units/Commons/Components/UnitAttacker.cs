using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Data;
using PanzerHero.Runtime.Units.Simultaneous;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Components
{
    public class UnitAttacker : BaseTimedAttackerComponent<UnitRig>
    {
        UnitPointersCollection _pointersCollection;
        
        UnitData data;
        
        UnitTargetCalculator targetCalculator;

        Timer fireTimer;
        
        public override void Initialize()
        {
            base.Initialize();
            
            targetCalculator = GetComponent<UnitTargetCalculator>();
            data = Rig.GetData();
            
            _pointersCollection = GetComponentInChildren<UnitPointersCollection>();

            fireTimer = new Timer(data.fireAttackDelay);
        }

        public bool IsTargetInFireRange()
        {
            return targetCalculator.IsTargetInRange(data.fireDistance);
        }

        public void TryFire(Vector3 targetPoint)
        {
            if (!fireTimer.CanDoAction())
            {
                return;
            }
            
            Fire_Internal(targetPoint);
            
            fireTimer.Reset();
        }
        
        void Fire_Internal(Vector3 targetPoint)
        {
            var point = _pointersCollection.firePoint;
            
            var prefab = data.bulletPrefab;
            
            var startPoint = point.position;
            var direction = (targetPoint - startPoint).normalized;

            var damage = data.fireDamage;
            
            SpawnBulletWithCustomDamage(prefab, damage, startPoint, direction);
        }
    }
}