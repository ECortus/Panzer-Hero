using GameDevUtils.Runtime.UI.Setups.Statistics;
using UnityEngine;

namespace PanzerHero.Runtime.Statistics
{
    public class PH_StatisticsFieldsSetup : StatisticsFieldsSetup
    {
        protected override void InitializeFields()
        {
            RegisterCurrentTimeText();
        }

        void RegisterCurrentTimeText()
        {
            var seconds = Time.time;
            RegisterRegularTimeStat("Play time", seconds);
        }
    }
}