using GameDevUtils.Runtime;
using UnityEngine;

namespace PanzerHero.Runtime.Statistics
{
    public class LevelStatistics : UnitySingleton<LevelStatistics>
    {
        public float PlayTime => Time.time;
    }
}