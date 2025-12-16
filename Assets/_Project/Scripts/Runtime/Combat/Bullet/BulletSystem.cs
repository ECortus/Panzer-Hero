using System.Collections.Generic;
using GameDevUtils.Runtime;

namespace PanzerHero.Runtime.Combat
{
    public class BulletSystem : AbstractComponentSystem<BulletBehaviour>
    {
        BulletManager manager;

        protected override void OnStart()
        {
            base.OnStart();
            manager = BulletManager.GetInstance;
        }

        protected override List<BulletBehaviour> GetUnitList()
        {
            return manager.AllComponents;
        }

        protected override bool UpdateFunction(BulletBehaviour unit, float delta)
        {
            unit.UpdateMethod(delta);
            return true;
        }
    }
}