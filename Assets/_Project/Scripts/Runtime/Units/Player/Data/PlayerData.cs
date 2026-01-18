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
        public float mainFireDelayAttack = 1;
        public float addditionalFireDelayAttack = 0.25f;
        
        [Space(5)]
        public BulletBehaviour rocketPrefab;
        public BulletBehaviour bulletPrefab;

        [Space(5)] 
        public VehicleEngineData engineData;
    }
}