using PanzerHero.Runtime.Combat;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player.Data
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "PanzerHero/Player/Data")]
    public class PlayerData : ScriptableObject
    {
        public float rotationSpeed = 15f;
        
        [Space(5)]
        public BulletBehaviour rocketPrefab;
        public BulletBehaviour bulletPrefab;
    }
}