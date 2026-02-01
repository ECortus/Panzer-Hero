using GameDevUtils.Runtime.UI;
using PanzerHero.Runtime.Currency;

namespace PanzerHero.UI.Debug
{
    public class PH_CheatsFieldsSetup : CheatsFieldsSetup
    {
        protected override void InitializeFields()
        {
            RegisterCoinSpawn();
            RegisterDiamondSpawn();
        }

        void RegisterCoinSpawn()
        {
            var coinsManager = CoinsManager.GetInstance;
            FieldManager.RegisterButton("Add 1000 coins", () => { coinsManager.Plus(1000); });
        }
        
        void RegisterDiamondSpawn()
        {
            var diamondsManager = DiamondsManager.GetInstance;
            FieldManager.RegisterButton("Add 100 diamonds", () => { diamondsManager.Plus(100); });
        }
    }
}