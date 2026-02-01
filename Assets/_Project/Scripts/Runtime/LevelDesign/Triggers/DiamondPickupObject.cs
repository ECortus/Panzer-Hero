using GameDevUtils.Runtime.Triggers;
using PanzerHero.Runtime.Currency;
using PanzerHero.Runtime.Statistics;

namespace PanzerHero.Runtime.LevelDesign.Triggers
{
    public class DiamondPickupObject : ResourceTriggerObject
    {
        LevelStatistics statistics;

        protected override void OnAwake()
        {
            base.OnAwake();
            statistics = LevelStatistics.GetInstance;
        }
        
        protected override void AddResource(float amount)
        {
            var manager = DiamondsManager.GetInstance;
            manager.Plus(amount);
            
            statistics.DiamondsPicked.Add(amount);
        }
    }
}