using PanzerHero.Runtime.Combat;
using PanzerHero.Runtime.Units.Abstract.Base;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Data
{
    [CreateAssetMenu(fileName = "New Unit Data", menuName = "Panzer Hero/Units/Commons/Data")]
    public class UnitData : ScriptableObject
    {
        public EUnitFaction Faction = EUnitFaction.Enemy;
        
        [Space(5)]
        public float maxHealth = 100f;

        [Space(5)] 
        public float movementSpeed = 5f;
        public float accelerationSpeed = 5f;
        public float angularSpeed = 180f;

        [Space(5)] 
        public BulletBehaviour bulletPrefab;

        [Space(5)] 
        public float fireDamage = 2f;
        public float fireAttackDelay = 0.25f;
        public float fireDistance = 10f;

        [Space(5)] 
        public float targetSearchingRadius = 60f;
    }
}