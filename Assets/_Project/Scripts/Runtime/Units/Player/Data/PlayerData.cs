using PanzerHero.Runtime.Combat;
using PanzerHero.Runtime.Units.Simultaneous;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player.Data
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Panzer Hero/Units/Player/Data")]
    public class PlayerData : ScriptableObject
    {
        public UpgradedCharactedData maxHealthData;
        public UpgradedCharactedData maxArmorData;
        
        [Space(5)]
        public float rotationSpeed = 15f;
        
        [Space(5)]
        public BulletBehaviour rocketPrefab;
        public int rocketsAmmoAmount = 3;
        public float rocketsReloadTime = 5f;
        
        public BulletBehaviour bulletPrefab;
        public int bulletsAmmoAmount = 100;
        public float bulletsReloadTime = 2f;
        
        [Space(2)]
        public UpgradedCharactedData reloadDurationModificatorUpgradeData;

        [Space(5)] 
        public float mainFireDamage = 5f;
        public float mainFireDelayAttack = 2;

        [Space(2)] 
        public float alternativeFireDamage = 0.25f;
        public float alternativeFireDelayAttack = 0.05f;

        [Space(2)] 
        public UpgradedCharactedData damageModificatorUpgradeData;

        [Space(5)] 
        public VehicleEngineData engineData;
    }
}