using BezierSolution;
using GameDevUtils.Runtime;
using UnityEngine;

namespace PanzerHero.Runtime.LevelDesign
{
    public class RoadSplineController : MonoBehaviour
    {
        [SerializeField] LevelEndTrigger endTrigger;
        
        BezierSpline bezierSpline;

        public void Initialize()
        {
            GetAllComponents();
            SpawnEndTrigger();
        }
        
        void GetAllComponents()
        {
            bezierSpline = GetComponentInChildren<BezierSpline>();
        }

        void SpawnEndTrigger()
        {
            var endT = 1f;
            var targetPosition = bezierSpline.GetPoint(endT);
            
            BezierSpline.Segment segment = bezierSpline.GetSegmentAt(endT);
            var targetRotation = Quaternion.LookRotation(segment.GetTangent(), segment.GetNormal());
            
            ObjectInstantiator.InstantiatePrefabForComponent(endTrigger, targetPosition, targetRotation, transform);
        }

        public BezierSpline GetSpline()
        {
            return bezierSpline;
        }
    }
}