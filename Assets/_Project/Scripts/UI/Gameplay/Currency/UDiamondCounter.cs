using GameDevUtils.Runtime.UI.Abstract;
using PanzerHero.Runtime.Currency;

namespace PanzerHero.UI.Currency
{
    public class UDiamondCounter : UDynamicTextField
    {
        DiamondsManager diamondsManager;
        
        protected override void OnStart()
        {
            base.OnStart();
            diamondsManager = DiamondsManager.GetInstance;
            
            diamondsManager.onChanged += UpdateField;
        }
        
        protected override string GetText()
        {
            return $"{diamondsManager.GetValueInt()}";
        }
    }
}