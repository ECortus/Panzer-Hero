using System;
using GameDevUtils.Runtime.UI.Abstract;
using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units;

namespace PanzerHero.UI.Gameplay.HUD
{
    public class UHealthSlider : UDynamicSliderField
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
            return player.Health.CurrentHealth / player.Health.MaxHealth;
        }

        protected override string GetLabelText()
        {
            var health = Math.Round(player.Health.CurrentHealth);
            var maxHealth = Math.Round(player.Health.MaxHealth);
            
            return $"{health}/{maxHealth}";
        }
    }
}