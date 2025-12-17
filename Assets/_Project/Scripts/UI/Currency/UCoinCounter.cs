using GameDevUtils.Runtime.UI.Abstract;
using PanzerHero.Runtime.Currency;

namespace PanzerHero.UI.Currency
{
    public class UCoinCounter : UDynamicTextField
    {
        CoinsManager coinsManager;
        
        protected override void OnStart()
        {
            base.OnStart();
            coinsManager = CoinsManager.GetInstance;
            
            coinsManager.onChanged += UpdateText;
        }
        
        protected override string GetText()
        {
            return $"{coinsManager.Value}";
        }
    }
}