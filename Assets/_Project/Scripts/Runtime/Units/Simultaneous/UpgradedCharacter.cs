using System;
using GameDevUtils.Runtime;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Simultaneous
{
    public interface IUpgradedCharacter
    {
        int ProgressLevel { get; }
        int MaxProgressLevel { get; }
        
        float PreviousProgressValue { get; }
        float PreviousProgressCost { get; }
        
        float CurrentProgressValue { get; }
        float CurrentProgressCost { get; }
        
        float NextProgressValue { get; }
        float NextProgressCost { get; }
        
        bool StayOnMinLevel { get; }
        bool ReachedMaxLevel { get; }
        
        bool CanUpgrade { get; }
        bool CanDegrade { get; }
        
        void Upgrade();
        void Reset();

        event Action OnChanged;
        event Action<float> OnValueChanged;
    }

    public class UpgradedCharacter : IUpgradedCharacter
    {
        UpgradedCharactedData data;
        
        int progressLevel;
        
        public int ProgressLevel => progressLevel;
        public int MaxProgressLevel => data.maxProgressLevel;

        public float PreviousProgressValue => progressLevel <= 0 ? -1 : data.progressValues[progressLevel - 1];
        public float PreviousProgressCost => progressLevel <= 0 ? -1 : data.progressCost[progressLevel - 1];

        public float CurrentProgressValue => data.progressValues[progressLevel];
        public float CurrentProgressCost => data.progressCost[progressLevel];
        
        public float NextProgressValue => progressLevel >= data.maxProgressLevel ? -1 : data.progressValues[progressLevel + 1];
        public float NextProgressCost => progressLevel >= data.maxProgressLevel ? -1 : data.progressCost[progressLevel + 1];

        public bool StayOnMinLevel => progressLevel <= 0;
        public bool ReachedMaxLevel => progressLevel >= data.maxProgressLevel;
        
        public bool CanUpgrade => progressLevel < data.maxProgressLevel;
        public bool CanDegrade => progressLevel > 0;

        public event Action OnChanged;
        public event Action<float> OnValueChanged;

        public UpgradedCharacter(UpgradedCharactedData data, int primaryLevel)
        {
            this.data = data;
            progressLevel = primaryLevel;
            
            OnUpdateValue();
        } 
        
        public void Upgrade()
        {
            if (progressLevel >= data.maxProgressLevel)
            {
                var info = data.Info;
                DebugHelper.LogWarning($"Trying upgrade character {info.Name} on MAX level!");
                return;
            }
            
            progressLevel++;
            OnUpdateValue();
        }

        public void Degrade()
        {
            if (progressLevel <= 0)
            {
                var info = data.Info;
                DebugHelper.LogWarning($"Trying degrade character {info.Name} on MIN level!");
                return;
            }

            progressLevel--;
            OnUpdateValue();
        }
        
        public void Reset()
        {
            progressLevel = 0;
            OnUpdateValue();
        }

        void OnUpdateValue()
        {
            var current = data.progressValues[progressLevel];
            
            OnChanged?.Invoke();
            OnValueChanged?.Invoke(current);
        }
    }
}