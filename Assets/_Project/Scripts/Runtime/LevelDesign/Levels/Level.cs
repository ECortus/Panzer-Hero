using System;
using System.Collections.Generic;
using System.Linq;
using BezierSolution;
using GameDevUtils.Runtime.Triggers;
using PanzerHero.Runtime.Destrictable;
using PanzerHero.Runtime.LevelDesign.Rewards;
using UnityEngine;

namespace PanzerHero.Runtime.LevelDesign.Levels
{
    public interface ILevelData
    {
        public BezierSpline RoadSpline { get; }

        public LevelReward Reward { get; }
    }
    
    public class Level : MonoBehaviour, ILevelData
    {
        [SerializeField] private LevelReward reward;

        [Header("--DEBUG--")] 
        [SerializeField] private TriggerObject[] triggerObjects;
        [SerializeField] private DestrictableObject[] destrictableObjects;
        
        public BezierSpline RoadSpline => roadSpline.GetSpline();
        public LevelReward Reward => reward;
        
        RoadSplineController roadSpline;

        public void Initialize()
        {
            GetAllComponents();
        }

        void GetAllComponents()
        {
            roadSpline = GetComponentInChildren<RoadSplineController>();
            roadSpline.Initialize();

            triggerObjects = GetComponentsInChildren<TriggerObject>();
            destrictableObjects = GetComponentsInChildren<DestrictableObject>();
        }

        public void Enable()
        {
            foreach (var trigger in triggerObjects)
            {
                trigger.Renew();
            }

            foreach (var destrictable in destrictableObjects)
            {
                destrictable.Renew();
            }
            
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
        
        public void Reseting()
        {
            //TODO: reset all level
        }
    }
}