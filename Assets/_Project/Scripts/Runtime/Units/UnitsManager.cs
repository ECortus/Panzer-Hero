using GameDevUtils.Runtime;
using PanzerHero.Runtime.Units.Abstract;
using PanzerHero.Runtime.Units.Abstract.Base;

namespace PanzerHero.Runtime.Units
{
    public class UnitsManager : AbstractComponentManager<IUnit, UnitsManager>
    {
        public IUnit Player { get; private set; }

        public override void Register(IUnit element)
        {
            base.Register(element);
            
            if (element.IsPlayer)
            {
                if (Player != null)
                    DebugHelper.LogError("Player already registered");

                Player = element;
            }
        }
    }
}