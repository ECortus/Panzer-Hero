using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Data;
using PanzerHero.Runtime.Units.Systems;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Components
{
    public class UnitTargetCalculator : BaseTargetCalculator<UnitRig>
    {
        UnitData data;
        
        public override void Initialize()
        {
            data = Rig.GetData();
            base.Initialize();
            
            var manager = UnitTargetCalculatorManager.GetInstance;
            manager.Register(this);
            
            var health = GetComponent<IHealth>();
            health.OnDied += () =>
            {
                manager.Unregister(this);
                ResetTarget();
            };
        }
        
        protected override LayerMask TargetLayer => LayerMask.GetMask("Player");
        protected override float Radius => data.targetSearchingRadius;
    }
}