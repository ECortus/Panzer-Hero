using GameDevUtils.Runtime.Triggers;
using PanzerHero.Runtime.Currency;

namespace PanzerHero.Runtime.LevelDesign.Triggers
{
    public class DiamondPickupObject : ResourceTriggerObject
    {
        protected override void AddResource(float amount)
        {
            var manager = DiamondsManager.GetInstance;
            manager.Plus(amount);
        }
    }
}