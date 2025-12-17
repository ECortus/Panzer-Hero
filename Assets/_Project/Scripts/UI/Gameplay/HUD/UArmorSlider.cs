using GameDevUtils.Runtime.UI.Abstract;
using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units;

namespace PanzerHero.UI.Gameplay.HUD
{
    public class UArmorSlider : UDynamicSliderField
    {
        UnitsManager unitsManager;
        IUnit player;
        
        protected override void OnStart()
        {
            base.OnStart();
            
            unitsManager = UnitsManager.GetInstance;
            player = unitsManager.Player;
        }
        
        protected override float GetSliderValue()
        {
            return player.Health.CurrentArmor / player.Health.MaxArmor;
        }
    }
}