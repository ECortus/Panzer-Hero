using GameDevUtils.Runtime;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Abstract.Base
{
    public interface IUnit : IManagedComponent
    {
        public bool IsAlive { get; }
        public Vector3 Position { get; }
        
        public EUnitFaction Faction { get; }
        public bool IsPlayer { get; }
        
        public bool IsFriendly(IUnit other);
        public bool IsOpposite(IUnit other);

        public IUnitHealth Health { get; }
    }
}