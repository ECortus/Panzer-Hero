using GameDevUtils.Runtime;
using GameDevUtils.Runtime.Simultaneous;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Simultaneous
{
    [CreateAssetMenu(fileName = "UpgradedCharactedData", menuName = "Panzer Hero/Units/Simultaneous/UpgradedCharactedData")]
    public class UpgradedCharactedData : ScriptableObject
    {
        [SerializeField] private ActorInformation info;

        [Space(10)] 
        [SerializeField] private float defaultValue = 1;
        
        [Space(5)]
        [SerializeField] private int stepCountPerProgress = 2;
        [SerializeField] private int maxProgressLevel = 5;

        [SerializeField] private float[] progressValues = new float[5];
        [SerializeField] private float[] progressCost = new float[5];
        
        public IActorInformation Info => info;
        
        public int StepCountPerProgress => stepCountPerProgress;
        public int MaxProgressLevel => maxProgressLevel;

        public float GetValue(int index)
        {
            if (index < 0 || index > progressValues.Length)
            {
                DebugHelper.LogWarning($"Invalid index on GetValue of {info.Name}");
                return -1f;
            }
            
            if (index == 0)
            {
                return defaultValue;
            }
            
            return progressValues[index - 1];
        }

        public float GetCost(int index)
        {
            if (index < 0 || index >= progressValues.Length)
            {
                DebugHelper.LogWarning($"Invalid index on GetCost of {info.Name}");
                return -1f;
            }

            return progressCost[index];
        }
    }
}