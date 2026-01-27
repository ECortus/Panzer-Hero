using System;
using GameDevUtils.Runtime.UI.Abstract;
using PanzerHero.Runtime.Currency;

namespace PanzerHero.UI.Currency
{
    public class UCoinCounter : UDynamicFloatField
    {
        CoinsManager coinsManager;
        
        protected override void OnStart()
        {
            coinsManager = CoinsManager.GetInstance;
            
            base.OnStart();
            coinsManager.onChanged += UpdateField;
        }
        
        protected override float GetTargetValue()
        {
            return coinsManager.GetValueInt();
        }

        protected void OnDestroy()
        {
            coinsManager.onChanged -= UpdateField;
        }
    }
}