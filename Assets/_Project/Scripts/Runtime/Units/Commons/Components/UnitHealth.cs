using GameDevUtils.Runtime;
using PanzerHero.Runtime.Statistics;
using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Data;

namespace PanzerHero.Runtime.Units.Components
{
    public class UnitHealth : BaseHealthComponent<UnitRig>
    {
        UnitData data;

        LevelStatistics statistics;
        
        public override void Initialize()
        {
            data = Rig.GetData();
            SetHealth(data.maxHealth);

            statistics = LevelStatistics.GetInstance;
        }

        protected override void Destroy()
        {
            statistics.EnemyKilled.AddSingular();
            base.Destroy();
        }
    }
}