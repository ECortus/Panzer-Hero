using GameDevUtils.Runtime.Triggers;
using PanzerHero.Runtime.Currency;

namespace PanzerHero.Runtime.LevelDesign.Triggers
{
    public class CoinPickupObject : ResourceTriggerObject
    {
        protected override void AddResource(float amount)
        {
            var manager = CoinsManager.GetInstance;
            manager.Plus(amount);
        }
    }
}