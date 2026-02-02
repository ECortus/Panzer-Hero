using System;
using GameDevUtils.Runtime.UI.Abstract;
using PanzerHero.Runtime.Units;
using PanzerHero.Runtime.Units.Interfaces;

namespace PanzerHero.UI.Gameplay.HUD
{
    public class UHealthSlider : UDynamicSliderField
    {
        protected virtual float Current => player.Health.CurrentHealth;
        protected virtual float Max => player.Health.MaxHealth;
        
        UnitsManager unitsManager;

        protected IPlayer player => unitsManager.Player;
        
        protected override void OnStart()
        {
            unitsManager = UnitsManager.GetInstance;
            base.OnStart();
        }
        
        protected override float GetSliderValue()
        {
            if (player == null)
            {
                return 0;
            }
            
            return Current / Max;
        }

        protected override string GetLabelText()
        {
            if (player == null)
            {
                return "0/0";
            }
            
            var current = MathF.Round(Current);
            var max = MathF.Round(Max);
            
            return $"{current}/{max}";
        }
    }
}