using GameDevUtils.Runtime.UI.Abstract;
using PanzerHero.Runtime.Currency;

namespace PanzerHero.UI.Currency
{
    public class UDiamondCounter : UDynamicFloatField
    {
        DiamondsManager diamondsManager;
        
        protected override void OnStart()
        {
            diamondsManager = DiamondsManager.GetInstance;
            
            base.OnStart();
            diamondsManager.onChanged += UpdateField;
        }
        
        protected override float GetTargetValue()
        {
            return diamondsManager.GetValueInt();
        }
    }
}