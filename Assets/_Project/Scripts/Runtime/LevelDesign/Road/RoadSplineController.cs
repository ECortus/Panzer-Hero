using BezierSolution;
using UnityEngine;

namespace PanzerHero.Runtime.LevelDesign
{
    public class RoadSplineController : MonoBehaviour
    {
        BezierSpline bezierSpline;

        public void Initialize()
        {
            GetAllComponents();
        }
        
        void GetAllComponents()
        {
            bezierSpline = GetComponentInChildren<BezierSpline>();
        }

        public BezierSpline GetSpline()
        {
            return bezierSpline;
        }
    }
}