using PanzerHero.Runtime.Abstract;
using PanzerHero.Runtime.Units.Components;

namespace PanzerHero.Runtime.Units
{
    public class UnitRig : BaseRig
    {
        protected override void InitializeComponents()
        {
            InitializeComponent<UnitHealth, UnitRig>();
            InitializeComponent<UnitMovement, UnitRig>();
            InitializeComponent<UnitAttacker, UnitRig>();
        }
    }
}