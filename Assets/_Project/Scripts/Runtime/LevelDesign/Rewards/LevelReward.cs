using System;
using GameDevUtils.Runtime;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PanzerHero.Runtime.LevelDesign.Rewards
{
    [CreateAssetMenu(fileName = "LevelReward 0", menuName = "Panzer Hero/LevelDesign/Rewards/LevelReward")]
    public class LevelReward : ScriptableObject
    {
        enum ERewardType
        {
            Standart, ScaledByGlobalValue, RandomFromLocalValues, RandomFromGlobalValues
        }

        [SerializeField] private ERewardType type;

        [SerializeField, DrawIf("type", ERewardType.Standart)] 
        private float standardCoinValue = 1;
        [SerializeField, DrawIf("type", ERewardType.Standart)] 
        private float standardDiamondValue = 1;

        [SerializeField, DrawIf("type", ERewardType.ScaledByGlobalValue)] 
        private float scaledGlobalValue = 1;

        [SerializeField, DrawIf("type", ERewardType.RandomFromLocalValues)] 
        private Vector2 randomCoinRange = new Vector2(1, 10);
        [SerializeField, DrawIf("type", ERewardType.RandomFromLocalValues)] 
        private Vector2 randomDiamondRange = new Vector2(1, 10);
        
        [SerializeField, DrawIf("type", ERewardType.RandomFromGlobalValues)] 
        private float scaledGlobalRandomValue = 1;

        public float GetCoinRewardValue()
        {
            float value = 0;
            var config = LevelDesignConfig.GetInstance;
            
            if (type == ERewardType.Standart)
            {
                value = standardCoinValue;
            }
            else if (type == ERewardType.ScaledByGlobalValue)
            {
                value = config.coinRewardValue * scaledGlobalValue;
            }
            else if (type == ERewardType.RandomFromLocalValues)
            {
                value = Random.Range(randomCoinRange.x, randomCoinRange.y);
            }
            else if (type == ERewardType.RandomFromGlobalValues)
            {
                var randomValue = Random.Range(config.coinRandomRewardRange.x, config.coinRandomRewardRange.y);
                value = randomValue * scaledGlobalRandomValue;
            }
            else
            {
                throw new NotImplementedException();
            }

            return value;
        }
        
        public float GetDiamondRewardValue()
        {
            float value = 0;
            var config = LevelDesignConfig.GetInstance;
            
            if (type == ERewardType.Standart)
            {
                value = standardDiamondValue;
            }
            else if (type == ERewardType.ScaledByGlobalValue)
            {
                value = config.diamondRewardValue * scaledGlobalValue;
            }
            else if (type == ERewardType.RandomFromLocalValues)
            {
                value = Random.Range(randomDiamondRange.x, randomDiamondRange.y);
            }
            else if (type == ERewardType.RandomFromGlobalValues)
            {
                var randomValue = Random.Range(config.diamondRandomRewardRange.x, config.diamondRandomRewardRange.y);
                value = randomValue * scaledGlobalRandomValue;
            }
            else
            {
                throw new NotImplementedException();
            }

            return value;
        }
    }
}