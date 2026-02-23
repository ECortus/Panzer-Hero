using System;
using PanzerHero.Runtime.SavePrefs;
using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Player.Data;
using PanzerHero.Runtime.Units.Player.Tank;
using PanzerHero.Runtime.Units.Simultaneous;
using SaveableExtension.Runtime.Saveable;

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

        TankModelController modelController;

        public override void Initialize()
        {
            base.Initialize();
            data = Rig.GetData();

            modelController = GetComponentInChildren<TankModelController>();

            maxHealth = CreateNewUpgrade(data.maxHealthData);
            maxArmor = CreateNewUpgrade(data.maxArmorData);
            damage = CreateNewUpgrade(data.damageModificatorUpgradeData);
            reloadDuration = CreateNewUpgrade(data.reloadDurationModificatorUpgradeData);

            maxHealth.OnChanged += SetBodyTier;
            maxArmor.OnChanged += SetBodyTier;
            damage.OnChanged += SetGunTier;
            reloadDuration.OnChanged += SetHeadTier;
            
            SaveableSupervisor.AddBehaviour(this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            SaveableSupervisor.RemoveBehaviour(this);
        }

        UpgradedCharacter CreateNewUpgrade(UpgradedCharactedData d)
        {
            return new UpgradedCharacter(d);
        }

        void SetHeadTier()
        {
            int tier = reloadDuration.ProgressLevel;
            SetHeadTier_Internal(tier);
        }

        void SetBodyTier()
        {
            int tier = (maxHealth.ProgressLevel + maxArmor.ProgressLevel) / 2;
            SetBodyTier_Internal(tier);
        }

        void SetGunTier()
        {
            int tier = damage.ProgressLevel;
            SetGunTier_Internal(tier);
        }

        void SetHeadTier_Internal(int tier)
        {
            modelController.SetHeadTier(tier);
        }
        
        void SetBodyTier_Internal(int tier)
        {
            modelController.SetBodyTier(tier);
        }
        
        void SetGunTier_Internal(int tier)
        {
            modelController.SetGunTier(tier);
        }

        public void Serialize(ref PanzerHeroPrefs record)
        {
            var skinPref = record.GetCurrentSkinPref();
            
            skinPref.MaxHealthGeneralLevel = maxHealth.GeneralLevel;
            skinPref.MaxArmorGeneralLevel = maxArmor.GeneralLevel;
            skinPref.DamageGeneralLevel = damage.GeneralLevel;
            skinPref.ReloadDurationGeneralLevel = reloadDuration.GeneralLevel;
            
            skinPref.MaxHealthProgressLevel = maxHealth.ProgressLevel;
            skinPref.MaxArmorProgressLevel = maxArmor.ProgressLevel;
            skinPref.DamageProgressLevel = damage.ProgressLevel;
            skinPref.ReloadDurationProgressLevel = reloadDuration.ProgressLevel;
            
            record.SetCurrentSkinPref(skinPref);
        }
        
        public void Deserialize(PanzerHeroPrefs record)
        {
            var skinPref = record.GetCurrentSkinPref();
            
            maxHealth.SetPrimaryLevel(skinPref.MaxHealthGeneralLevel);
            maxArmor.SetPrimaryLevel(skinPref.MaxArmorGeneralLevel);
            damage.SetPrimaryLevel(skinPref.DamageGeneralLevel);
            reloadDuration.SetPrimaryLevel(skinPref.ReloadDurationGeneralLevel);
        }

        #region Interface

        public IUpgradedCharacter MaxHealth => maxHealth;
        public IUpgradedCharacter MaxArmor => maxArmor;
        public IUpgradedCharacter Damage => damage;
        public IUpgradedCharacter ReloadDuration => reloadDuration;

        #endregion
    }
}