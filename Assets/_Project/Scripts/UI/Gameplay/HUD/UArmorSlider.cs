using System;
using GameDevUtils.Runtime.UI.Abstract;
using PanzerHero.Runtime.Units;
using PanzerHero.Runtime.Units.Interfaces;

namespace PanzerHero.UI.Gameplay.HUD
{
    public class UArmorSlider : UHealthSlider
    {
        protected override float Current => player.Health.CurrentArmor;
        protected override float Max => player.Health.MaxArmor;
    }
}