using GameDevUtils.Runtime.UI.Setups.Statistics;
using UnityEngine;

namespace PanzerHero.Runtime.Statistics
{
    public class PH_StatisticsFieldsSetup : StatisticsFieldsSetup
    {
        LevelStatistics statistics;
        
        protected override void InitializeFields()
        {
            statistics = LevelStatistics.GetInstance;
            RegisterCurrentTimeText();
        }

        void RegisterCurrentTimeText()
        {
            var time = statistics.PlayTime;
            RegisterRegularTimeStat("Play time", time);
        }
    }
}