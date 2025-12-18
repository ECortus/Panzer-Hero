using GameDevUtils.Runtime;
using GameSaveKit.Runtime.Saveable;
using PanzerHero.Runtime.SavePrefs;

namespace PanzerHero.Runtime.Currency
{
    public class DiamondsManager : AbstractResourceManager<DiamondsManager>, ISaveableBehaviour<PanzerHeroPrefs>
    {
        private void Awake()
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