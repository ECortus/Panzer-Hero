using GameDevUtils.Runtime.UI;
using PanzerHero.Runtime.Currency;

namespace PanzerHero.Runtime.Debug
{
    public class PH_CheatsFieldSetup : CheatsFieldSetup
    {
        protected override void Setup()
        {
            RegisterCoinSpawn();
            RegisterDiamondSpawn();
        }

        void RegisterCoinSpawn()
        {
            var coinsManager = CoinsManager.GetInstance;
            FieldManager.RegisterDebugButton("Add 1000 coins", () => { coinsManager.Plus(1000); });
        }
        
        void RegisterDiamondSpawn()
        {
            var diamondsManager = DiamondsManager.GetInstance;
            FieldManager.RegisterDebugButton("Add 100 diamonds", () => { diamondsManager.Plus(100); });
        }
    }
}