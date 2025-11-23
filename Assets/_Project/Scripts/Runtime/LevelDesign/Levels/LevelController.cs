using System;
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
            roadSpline = GetComponent<RoadSplineController>();
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
        
        public IRoadSplineData GetRoadSplineData()
        {
            return roadSpline;
        }
    }
}