using PanzerHero.Runtime.Combat;
using PanzerHero.Runtime.Units.Simultaneous;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player.Data
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Panzer Hero/Player/Data")]
    public class PlayerData : ScriptableObject
    {
        public float maxHealth = 100f;
        public float maxArmor = 50f;
        
        [Space(5)]
        public float rotationSpeed = 15f;
        
        [Space(5)]
        public BulletBehaviour rocketPrefab;
        public int rocketsAmmoAmount = 3;
        public float rocketsReloadTime = 5f;
        
        public BulletBehaviour bulletPrefab;
        public int bulletsAmmoAmount = 100;
        public float bulletsReloadTime = 2f;

        [Space(5)] 
        public float mainFireDamage = 5f;
        public float mainFireDelayAttack = 2;

        [Space(2)] 
        public float alternativeFireDamage = 0.25f;
        public float alternativeFireDelayAttack = 0.05f;

        [Space(5)] 
        public VehicleEngineData engineData;
    }
}