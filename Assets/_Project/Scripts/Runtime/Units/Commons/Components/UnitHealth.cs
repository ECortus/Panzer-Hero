using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Data;

namespace PanzerHero.Runtime.Units.Components
{
    public class UnitHealth : BaseHealthComponent<UnitRig>
    {
        UnitData data;
        
        public override void Initialize()
        {
            data = Rig.GetData();
            SetHealth(data.maxHealth);
        }
    }
}