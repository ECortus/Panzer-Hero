using System;
using BezierSolution;
using UnityEngine;

namespace PanzerHero.Runtime.LevelDesign.Levels
{
    public class LevelController : MonoBehaviour
    {
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
        
        public BezierSpline GetRoadSpline()
        {
            return roadSpline.GetSpline();
        }
    }
}