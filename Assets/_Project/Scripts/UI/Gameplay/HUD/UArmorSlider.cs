using GameDevUtils.Runtime.UI.Abstract;

namespace PanzerHero.UI.Gameplay.HUD
{
    public class UArmorSlider : UDynamicSliderField
    {
        protected override void OnStart()
        {
            base.OnStart();
        }
        
        protected override float GetSliderValue()
        {
            return 1f;
        }
    }
}