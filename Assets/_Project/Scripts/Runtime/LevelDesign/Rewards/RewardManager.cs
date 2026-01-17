using System;
using GameDevUtils.Runtime;
using PanzerHero.Runtime.Currency;
using PanzerHero.Runtime.LevelDesign.Levels;
using UnityEngine;

namespace PanzerHero.Runtime.LevelDesign.Rewards
{
    public class RewardManager : UnitySingleton<RewardManager>
    {
        GameStatement statement;
        LevelManager levelManager;
        
        CoinsManager coinsManager;
        DiamondsManager diamondsManager;

        int rewardedCoins = -1;
        int rewardedDiamonds = -1;
        
        void Awake()
        {
            statement = GameStatement.GetInstance;
            levelManager = LevelManager.GetInstance;

            coinsManager = CoinsManager.GetInstance;
            diamondsManager = DiamondsManager.GetInstance;

            statement.OnGameFinished += RewardProcess;
        }

        void RewardProcess()
        {
            var levelData = levelManager.GetLevelData();
            var rewardData = levelData.Reward;

            var coin = rewardData.GetCoinRewardValue();
            var diamond = rewardData.GetDiamondRewardValue();

            var coinRounded = Mathf.CeilToInt(coin);
            var diamondRounded = Mathf.CeilToInt(diamond);

            rewardedCoins = coinRounded;
            rewardedDiamonds = diamondRounded;
            
            coinsManager.Plus(coinRounded);
            diamondsManager.Plus(diamondRounded);
        }

        public int GetRewardedCoinsValue() => rewardedCoins;
        public int GetRewardedDiamondsValue() => rewardedDiamonds;
    }
}