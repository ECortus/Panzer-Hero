using GameSaveKit.Runtime.Saveable;
using PanzerHero.Runtime.SavePrefs;
using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Player.Data;
using PanzerHero.Runtime.Units.Simultaneous;

namespace PanzerHero.Runtime.Units.Player.Components
{
    public interface IPlayerUpgradedCharacters
    {
        IUpgradedCharacter MaxHealth { get; }
        IUpgradedCharacter MaxArmor { get; }
        
        IUpgradedCharacter Damage { get; }
        IUpgradedCharacter ReloadDuration { get; }
    }
    
    public class PlayerUpgradedCharacters : BaseRigComponent<PlayerRig>, IPlayerUpgradedCharacters, ISaveableBehaviour<PanzerHeroPrefs>
    {
        PlayerData data;
        
        UpgradedCharacter maxHealth;
        UpgradedCharacter maxArmor;
        
        UpgradedCharacter damage;
        UpgradedCharacter reloadDuration;

        public override void Initialize()
        {
            base.Initialize();
            data = Rig.GetData();
            
            SaveableSupervisor.AddBehaviour(this);
        }
        
        UpgradedCharacter CreateNewUpgrade(UpgradedCharactedData data, int lvl)
        {
            return new UpgradedCharacter(data, lvl);
        }

        public void Serialize(ref PanzerHeroPrefs record)
        {
            record.MaxHealthProgressLevel = maxHealth.ProgressLevel;
            record.MaxArmorProgressLevel = maxArmor.ProgressLevel;
            record.DamageProgressLevel = damage.ProgressLevel;
            record.ReloadDurationProgressLevel = reloadDuration.ProgressLevel;
        }
        
        public void Deserialize(PanzerHeroPrefs record)
        {
            maxHealth = CreateNewUpgrade(data.maxHealthData, record.MaxHealthProgressLevel);
            maxArmor = CreateNewUpgrade(data.maxArmorData, record.MaxArmorProgressLevel);
            damage = CreateNewUpgrade(data.damageModificatorUpgradeData, record.DamageProgressLevel);
            reloadDuration = CreateNewUpgrade(data.reloadDurationModificatorUpgradeData, record.ReloadDurationProgressLevel);
        }

        #region Interface

        public IUpgradedCharacter MaxHealth => maxHealth;
        public IUpgradedCharacter MaxArmor => maxArmor;
        public IUpgradedCharacter Damage => damage;
        public IUpgradedCharacter ReloadDuration => reloadDuration;

        #endregion
    }
}