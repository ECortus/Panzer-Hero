using System;
using GameDevUtils.Runtime;
using GameSaveKit.Runtime.Saveable;
using PanzerHero.Runtime.SavePrefs;

namespace PanzerHero.Runtime.Currency
{
    public class CoinsManager : AbstractResourceManager<CoinsManager>, ISaveableBehaviour<PanzerHeroPrefs>
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
            record.Coins = GetValueInt();
        }
        
        public void Deserialize(PanzerHeroPrefs record)
        {
            SetValue(record.Coins);
        }
    }
}