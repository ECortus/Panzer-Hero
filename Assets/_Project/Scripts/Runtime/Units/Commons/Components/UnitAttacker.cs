using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Data;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Components
{
    public class UnitAttacker : BaseAttackerComponent<UnitRig>
    {
        UnitPointersCollection _pointersCollection;
        
        UnitData data;
        
        public override void Initialize()
        {
            base.Initialize();
            data = Rig.GetData();
            
            _pointersCollection = GetComponentInChildren<UnitPointersCollection>();
        }

        public void Fire(Vector3 targetPoint)
        {
            Fire_Internal(targetPoint);
        }
        
        void Fire_Internal(Vector3 targetPoint)
        {
            var point = _pointersCollection.firePoint;
            
            var prefab = data.bulletPrefab;
            
            var startPoint = point.position;
            var direction = (targetPoint - startPoint).normalized;
            
            SpawnBullet(prefab, startPoint, direction);
        }
    }
}