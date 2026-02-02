using GameDevUtils.Runtime;
using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Player.Data;
using PanzerHero.Runtime.Units.Player.Tank;
using PanzerHero.Runtime.Units.Simultaneous;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player.Components
{
    public class PlayerHealth : BaseHealthComponent<PlayerRig>
    {
        IUpgradedCharacter healthCharacter;
        IUpgradedCharacter armorCharacter;

        TankModelController modelController;

        VehicleEngine engine;
        Collider[] colliders;
        
        public override void Initialize()
        {
            engine = GetComponent<VehicleEngine>();
            modelController = GetComponentInChildren<TankModelController>();
            colliders = GetComponents<Collider>();
            
            var characters = GetComponent<PlayerUpgradedCharacters>();

            healthCharacter = characters.MaxHealth;
            armorCharacter = characters.MaxArmor;
            
            SetHealth(healthCharacter.CurrentProgressValue);
            healthCharacter.OnValueChanged += ((f) => SetHealth(f));
            
            SetArmor(armorCharacter.CurrentProgressValue);
            armorCharacter.OnValueChanged += ((f) => SetArmor(f));
        }

        protected override void Destroy()
        {
            engine.enabled = false;
            foreach (var col in colliders)
            {
                col.enabled = false;
            }
            
            modelController.SetDead();
        }
    }
}