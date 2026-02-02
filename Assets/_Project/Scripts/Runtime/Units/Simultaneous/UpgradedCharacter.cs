using System;
using GameDevUtils.Runtime;
using GameDevUtils.Runtime.Simultaneous;
using JetBrains.Annotations;
using PanzerHero.Runtime.Currency;
using UnityEngine;
using UnityEngine.Events;

namespace PanzerHero.Runtime.Units.Simultaneous
{
    public interface IUpgradedCharacter
    {
        IActorInformation Info { get; }
        
        int GeneralLevel { get; }
        int StepLevel { get; }
        int ProgressLevel { get; }
        
        int StepCountPerProgress { get; }
        int MaxProgressLevel { get; }
        
        float PreviousProgressValue { get; }
        float PreviousProgressCost { get; }
        
        float CurrentProgressValue { get; }
        float CurrentProgressCost { get; }
        
        float NextProgressValue { get; }
        float NextProgressCost { get; }
        
        bool StayOnMinProgress { get; }
        bool ReachedMaxProgress { get; }
        
        bool CanUpgrade { get; }
        bool CanDegrade { get; }
        
        void Upgrade();
        void Degrade();
        void Reset();

        event Action OnChanged;
        event Action<float> OnValueChanged;
    }

    public class UpgradedCharacter : IUpgradedCharacter
    {
        CoinsManager coinsManager;
        
        UpgradedCharactedData data;

        int generalLevel;

        int stepCount => data.StepCountPerProgress;
        int maxGeneralLevel => data.MaxProgressLevel * data.StepCountPerProgress;

        int stepLevel => generalLevel % stepCount;
        int progressLevel => generalLevel / stepCount;
        
        bool canUpgrade
        {
            get
            {
                var cost = CurrentProgressCost;
                
                var notMaxLevel = generalLevel < maxGeneralLevel;
                var enoughCoin = coinsManager.HasRequiredAmount(cost);

                return notMaxLevel && enoughCoin;
            }
        }
        bool canDegrade => generalLevel > 0;

        public UpgradedCharacter(UpgradedCharactedData data)
        {
            this.data = data;
            coinsManager = CoinsManager.GetInstance;
            
            OnUpdateValue();
        }

        public void SetPrimaryLevel(int primaryGeneralLevel)
        {
            generalLevel = primaryGeneralLevel;
            OnUpdateValue();
        }
        
        public void Upgrade()
        {
            if (generalLevel >= maxGeneralLevel)
            {
                var info = data.Info;
                DebugHelper.LogWarning($"Trying upgrade character {info.Name} on MAX level!");
                return;
            }

            if (!canUpgrade)
            {
                return;
            }

            var cost = CurrentProgressCost;
            coinsManager.Reduce(cost);
            
            generalLevel++;
            OnUpdateValue();
        }

        public void Degrade()
        {
            if (generalLevel <= 0)
            {
                var info = data.Info;
                DebugHelper.LogWarning($"Trying degrade character {info.Name} on MIN level!");
                return;
            }

            if (!canDegrade)
            {
                return;
            }
            
            var cost = CurrentProgressCost;
            coinsManager.Plus(cost);

            generalLevel--;
            OnUpdateValue();
        }
        
        public void Reset()
        {
            generalLevel = 0;
            OnUpdateValue();
        }

        void OnUpdateValue()
        {
            float currentValue = CurrentProgressValue;
            
            OnChanged?.Invoke();
            OnValueChanged?.Invoke(currentValue);
        }
        
        #region Interface

        public IActorInformation Info => data.Info;

        public int GeneralLevel => generalLevel;
        public int StepLevel => stepLevel;
        public int ProgressLevel => progressLevel;

        public int StepCountPerProgress => stepCount;
        public int MaxProgressLevel => data.MaxProgressLevel;

        public float PreviousProgressValue => progressLevel <= 0 ? -1 : data.GetValue(progressLevel - 1);
        public float PreviousProgressCost => progressLevel <= 0 ? -1 : data.GetCost(progressLevel - 1);

        public float CurrentProgressValue => data.GetValue(progressLevel);
        public float CurrentProgressCost => data.GetCost(progressLevel);
        
        public float NextProgressValue => progressLevel >= data.MaxProgressLevel ? -1 : data.GetValue(progressLevel + 1);
        public float NextProgressCost => progressLevel >= data.MaxProgressLevel ? -1 : data.GetCost(progressLevel + 1);

        public bool StayOnMinProgress => progressLevel <= 0;
        public bool ReachedMaxProgress => progressLevel >= data.MaxProgressLevel;

        public bool CanUpgrade => canUpgrade;
        public bool CanDegrade => canDegrade;
        
        public event Action OnChanged;
        public event Action<float> OnValueChanged;

        #endregion
    }
}