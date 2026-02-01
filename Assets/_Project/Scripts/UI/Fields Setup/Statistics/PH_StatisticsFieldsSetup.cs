using GameDevUtils.Runtime.UI.Setups.Statistics;
using PanzerHero.Runtime.Statistics;
using UnityEngine;

namespace PanzerHero.UI.Statistics
{
    public class PH_StatisticsFieldsSetup : StatisticsFieldsSetup
    {
        LevelStatistics statistics;
        
        protected override void InitializeFields()
        {
            statistics = LevelStatistics.GetInstance;
            
            RegisterPlayTime();
            RegisterEnemyKilled();
            RegisterHousesDestroyed();
            RegisterCoinsPicked();
            RegisterDiamondPicked();
        }

        void RegisterPlayTime()
        {
            var playTime = statistics.PlayTime;
            RegisterRegularTimeStat("Play Time", playTime);
        }

        void RegisterEnemyKilled()
        {
            int killed = statistics.EnemyKilled.GetValueInt();
            RegisterIntStat("Enemies", killed);
        }

        void RegisterHousesDestroyed()
        {
            int destroyed = statistics.HousesDestroyed.GetValueInt();
            RegisterIntStat("Houses", destroyed);
        }

        void RegisterCoinsPicked()
        {
            var coins = statistics.CoinsPicked.GetValue();
            RegisterFloatStat("Coins", coins, 0);
        }

        void RegisterDiamondPicked()
        {
            var diamonds = statistics.DiamondsPicked.GetValue();
            RegisterFloatStat("Diamons", diamonds, 0);
        }
    }
}