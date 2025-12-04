using System;
using BezierSolution;
using UnityEngine;

namespace PanzerHero.Runtime.LevelDesign.Levels
{
    public interface ILevelData
    {
        public BezierSpline RoadSpline { get; }
    }
    
    public class LevelController : MonoBehaviour, ILevelData
    {
        public BezierSpline RoadSpline => roadSpline.GetSpline();
        
        RoadSplineController roadSpline;

        public void Initialize()
        {
            GetAllComponents();
        }

        void GetAllComponents()
        {
            roadSpline = GetComponentInChildren<RoadSplineController>();
            roadSpline.Initialize();
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void RestartLevel()
        {
            
        }
    }
}