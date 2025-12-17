using GameDevUtils.Runtime;

namespace PanzerHero.Runtime.Units.Abstract.Base
{
    public interface IUnit : IManagedComponent
    {
        public EUnitFaction Faction { get; }
        public bool IsPlayer { get; }
        
        public bool IsFriendly(IUnit other);
        public bool IsOpposite(IUnit other);

        public IUnitHealth Health { get; }
    }
}