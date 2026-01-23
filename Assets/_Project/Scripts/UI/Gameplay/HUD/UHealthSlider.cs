using System;
using GameDevUtils.Runtime.UI.Abstract;
using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units;

namespace PanzerHero.UI.Gameplay.HUD
{
    public class UHealthSlider : UDynamicSliderField
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
            base.OnStart();
            unitsManager = UnitsManager.GetInstance;
        }
        
        protected override float GetSliderValue()
        {
            if (player == null)
            {
                return 0;
            }
            
            return player.Health.CurrentHealth / player.Health.MaxHealth;
        }

        protected override string GetLabelText()
        {
            if (player == null)
            {
                return "0/0";
            }
            
            var health = Math.Round(player.Health.CurrentHealth);
            var maxHealth = Math.Round(player.Health.MaxHealth);
            
            return $"{health}/{maxHealth}";
        }
    }
}