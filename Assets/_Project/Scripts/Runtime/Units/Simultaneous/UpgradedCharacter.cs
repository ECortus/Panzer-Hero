using System;
using GameDevUtils.Runtime;
using GameDevUtils.Runtime.Simultaneous;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace PanzerHero.Runtime.Units.Simultaneous
{
    public interface IUpgradedCharacter
    {
        IActorInformation Info { get; }
        
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

        event EventHandler OnChanged;
        event EventHandler<float> OnValueChanged;
    }

    public class UpgradedCharacter : IUpgradedCharacter
    {
        UpgradedCharactedData data;

        int generalLevel;

        int stepCount => data.StepCountPerProgress;

        int stepLevel => generalLevel % stepCount;
        int progressLevel => generalLevel / stepCount;
        
        public IActorInformation Info => data.Info;

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

        public bool CanUpgrade => generalLevel < data.MaxProgressLevel * data.StepCountPerProgress;
        public bool CanDegrade => generalLevel > 0;

        event EventHandler _onChanged;
        event EventHandler<float> _onValueChanged;
        
        public event EventHandler OnChanged
        {
            add => _onChanged += value;
            remove => _onChanged -= value;
        }
        public event EventHandler<float> OnValueChanged
        {
            add
            {
                _onValueChanged += value;
                DebugHelper.LogWarning("ADD NEW EVENT TO ON VALUE CHANGED");
            }
            remove
            {
                _onValueChanged -= value; 
                DebugHelper.LogWarning("REMOVE NEW EVENT TO ON VALUE CHANGED");
            }
        }

        public UpgradedCharacter(UpgradedCharactedData data, int primaryGeneralLevel)
        {
            this.data = data;
            generalLevel = primaryGeneralLevel;
            
            OnUpdateValue();
        } 
        
        public void Upgrade()
        {
            if (generalLevel >= data.MaxProgressLevel * data.StepCountPerProgress)
            {
                var info = data.Info;
                DebugHelper.LogWarning($"Trying upgrade character {info.Name} on MAX level!");
                return;
            }
            
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
            
            _onChanged?.Invoke(this, EventArgs.Empty);
            _onValueChanged?.Invoke(this, currentValue);
        }
    }
}