using GameDevUtils.Runtime;
using PanzerHero.Runtime.SavePrefs;
using SaveableExtension.Runtime.Saveable;

namespace PanzerHero.Runtime.Currency
{
    public class DiamondsManager : AbstractResourceManager<DiamondsManager>, ISaveableBehaviour<PanzerHeroPrefs>
    {
        protected override void OnAwake()
        {
            SaveableSupervisor.AddBehaviour(this);
        }

        private void OnDestroy()
        {
            SaveableSupervisor.RemoveBehaviour(this);
        }
        
        public void Serialize(ref PanzerHeroPrefs record)
        {
            record.Diamonds = GetValueInt();
        }
        
        public void Deserialize(PanzerHeroPrefs record)
        {
            SetValue(record.Diamonds);
        }
    }
}