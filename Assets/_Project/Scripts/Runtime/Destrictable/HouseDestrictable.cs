using PanzerHero.Runtime.Statistics;

namespace PanzerHero.Runtime.Destrictable
{
    public class HouseDestrictable : DestrictableObject
    {
        LevelStatistics statistics;

        protected override void Awake()
        {
            statistics = LevelStatistics.GetInstance;
            base.Awake();
        }

        public override void Destroy()
        {
            statistics.HousesDestroyed.AddSingular();
            base.Destroy();
        }
    }
}