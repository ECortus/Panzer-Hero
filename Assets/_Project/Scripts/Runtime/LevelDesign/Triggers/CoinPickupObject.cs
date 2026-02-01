using GameDevUtils.Runtime.Triggers;
using PanzerHero.Runtime.Currency;
using PanzerHero.Runtime.Statistics;

namespace PanzerHero.Runtime.LevelDesign.Triggers
{
    public class CoinPickupObject : ResourceTriggerObject
    {
        LevelStatistics statistics;

        protected override void OnAwake()
        {
            base.OnAwake();
            statistics = LevelStatistics.GetInstance;
        }

        protected override void AddResource(float amount)
        {
            var manager = CoinsManager.GetInstance;
            manager.Plus(amount);
            
            statistics.CoinsPicked.Add(amount);
        }
    }
}