using GameDevUtils.Runtime.UI.Abstract;
using PanzerHero.Runtime.Units;
using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Interfaces;
using PanzerHero.Runtime.Units.Player.Components;
using UnityEngine;
using UnityEngine.UI;

namespace PanzerHero.UI.Gameplay.HUD
{
    public class URocketAmmoCounter : UAmmoCounter
    {
        protected override IAmmo Ammo => player.Ammo.Rockets;
        protected override ITimerInfo TimerInfo => player.Attacker.MainFireTimerInfo;
    }
}