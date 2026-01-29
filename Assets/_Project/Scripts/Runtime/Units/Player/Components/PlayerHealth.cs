using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Player.Data;
using PanzerHero.Runtime.Units.Simultaneous;

namespace PanzerHero.Runtime.Units.Player.Components
{
    public class PlayerHealth : BaseHealthComponent<PlayerRig>
    {
        IUpgradedCharacter healthCharacter;
        IUpgradedCharacter armorCharacter;
        
        public override void Initialize()
        {
            var characters = GetComponent<IPlayerUpgradedCharacters>();

            healthCharacter = characters.MaxHealth;
            armorCharacter = characters.MaxArmor;
            
            SetHealth(healthCharacter.CurrentProgressValue);
            healthCharacter.OnValueChanged += ((sender, f) => SetHealth(f));
            
            SetArmor(armorCharacter.CurrentProgressValue);
            armorCharacter.OnValueChanged += ((sender, f) => SetArmor(f));
        }
    }
}