using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Player.Data;

namespace PanzerHero.Runtime.Units.Player.Components
{
    public class PlayerHealth : BaseHealthComponent<PlayerRig>
    {
        PlayerData data;
        
        public override void Initialize()
        {
            data = Rig.GetData();
            
            SetHealth(data.maxHealth);
            SetArmor(data.maxArmor);
        }
    }
}