using System.Collections.Generic;
using GameDevUtils.Runtime;
using PanzerHero.Runtime.Units.Interfaces;

namespace PanzerHero.Runtime.Units
{
    public class UnitsManager : AbstractComponentManager<IUnit, UnitsManager>
    {
        public IPlayer Player { get; private set; }

        public List<IUnit> PlayerFaction { get; private set; } = new List<IUnit>();
        public List<IUnit> EnemyFaction { get; private set; } = new List<IUnit>();

        public override void Register(IUnit element)
        {
            base.Register(element);
            
            if (element is IPlayer player)
            {
                if (Player != null)
                    DebugHelper.LogError("Player already registered");

                Player = player;
            }

            if (element.Faction == EUnitFaction.Player)
            {
                PlayerFaction.Add(element);
            }
            else if (element.Faction == EUnitFaction.Enemy)
            {
                EnemyFaction.Add(element);
            }
        }

        public override void Unregister(IUnit element)
        {
            base.Unregister(element);
            
            if (element is IPlayer)
            {
                if (Player == null)
                    DebugHelper.LogError("Player don't registered");

                Player = null;
            }
            
            if (element.Faction == EUnitFaction.Player)
            {
                PlayerFaction.Remove(element);
            }
            else if (element.Faction == EUnitFaction.Enemy)
            {
                EnemyFaction.Remove(element);
            }
        }
    }
}