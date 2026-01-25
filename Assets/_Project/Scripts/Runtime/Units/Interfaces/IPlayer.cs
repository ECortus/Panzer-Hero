using PanzerHero.Runtime.Units.Player.Components;

namespace PanzerHero.Runtime.Units.Interfaces
{
    public interface IPlayer : IUnit
    {
        public IPlayerAmmo Ammo { get; }
        public IPLayerAttacker Attacker { get; }
    }
}