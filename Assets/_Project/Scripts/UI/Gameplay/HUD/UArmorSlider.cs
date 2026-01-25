using System;
using GameDevUtils.Runtime.UI.Abstract;
using PanzerHero.Runtime.Units;
using PanzerHero.Runtime.Units.Interfaces;

namespace PanzerHero.UI.Gameplay.HUD
{
    public class UArmorSlider : UDynamicSliderField
    {
        UnitsManager unitsManager;
        
        IUnit _player;
        IUnit player
        {
            get
            {
                if (_player == null)
                {
                    _player = unitsManager.Player;
                }

                return _player;
            }
        }
        
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
            
            return player.Health.CurrentArmor / player.Health.MaxArmor;
        }
        
        protected override string GetLabelText()
        {
            if (player == null)
            {
                return "0/0";
            }
            
            var health = Math.Round(player.Health.CurrentArmor);
            var maxHealth = Math.Round(player.Health.MaxArmor);
            
            return $"{health}/{maxHealth}";
        }
    }
}