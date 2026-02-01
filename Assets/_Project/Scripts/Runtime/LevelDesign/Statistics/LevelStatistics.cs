using GameDevUtils.Runtime;
using PanzerHero.Runtime.LevelDesign;
using UnityEngine;

namespace PanzerHero.Runtime.Statistics
{
    public class LevelStatistics : UnitySingleton<LevelStatistics>
    {
        GameStatement gameStatement;

        float lastTimeOfReset;
        public float PlayTime => Time.time - lastTimeOfReset;

        public Character EnemyKilled { get; private set; } = new Character();
        public Character HousesDestroyed { get; private set; } = new Character();
        
        public Character CoinsPicked { get; private set; } = new Character();
        public Character DiamondsPicked { get; private set; } = new Character();
        
        protected override void OnAwake()
        {
            gameStatement = GameStatement.GetInstance;
            base.OnAwake();

            gameStatement.OnGameStarted += ResetStats;
        }

        void ResetStats()
        {
            lastTimeOfReset = Time.time;
            
            EnemyKilled.Reset();
            HousesDestroyed.Reset();
            
            CoinsPicked.Reset();
            DiamondsPicked.Reset();
        }

        public class Character
        {
            float value;

            public void Add(float add)
            {
                value += add;
            }

            public void AddSingular()
            {
                value++;
            }

            public void Reset()
            {
                value = 0;
            }

            public float GetValue() => value;
            public int GetValueInt() => Mathf.RoundToInt(value);
        }
    }
}