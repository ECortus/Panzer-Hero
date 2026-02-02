using GameDevUtils.Runtime.UI.Abstract;
using PanzerHero.Runtime.Units;
using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Interfaces;
using PanzerHero.Runtime.Units.Player.Components;
using PanzerHero.Runtime.Units.Simultaneous;
using UnityEngine;
using UnityEngine.UI;

namespace PanzerHero.UI.Gameplay.HUD
{
    public class UBulletAmmoCounter : UAmmoCounter
    {
        protected override IAmmo Ammo => player != null ? player.Ammo.Bullets : null;
        protected override ITimerInfo TimerInfo => player != null ? player.Attacker.AlternativeFireTimerInfo : null;
    }
}