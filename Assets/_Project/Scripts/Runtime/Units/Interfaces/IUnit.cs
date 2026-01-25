using GameDevUtils.Runtime;
using PanzerHero.Runtime.Units.Abstract.Base;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Interfaces
{
    public interface IUnit : IManagedComponent
    {
        public bool IsAlive { get; }
        public Vector3 Position { get; }
        
        public EUnitFaction Faction { get; }
        
        public bool IsFriendly(IUnit other);
        public bool IsOpposite(IUnit other);

        public IHealth Health { get; }
    }
}