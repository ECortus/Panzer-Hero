using GameDevUtils.Runtime;
using UnityEngine;

namespace PanzerHero.Runtime.LevelDesign
{
    [CreateAssetMenu(fileName = "LevelDesignConfig", menuName = "Panzer Hero/Configs/Level Design")]
    public class LevelDesignConfig : UnityScriptableSingleton<LevelDesignConfig>
    {
        [Header("Rewards economy")] 
        public float coinRewardValue = 100;
        public float diamondRewardValue = 100;
        
        [Space(5)]
        public Vector2 coinRandomRewardRange = new Vector2(10, 100);
        public Vector2 diamondRandomRewardRange = new Vector2(10, 100);
    }
}