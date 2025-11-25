using BezierSolution;
using PanzerHero.Runtime.LevelDesign.Levels;
using PanzerHero.Runtime.Units.Components;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player
{
    public class PlayerBezierSpline : RigComponent
    {
        [SerializeField] BezierSpline bezierSpline;
        [SerializeField] float normalizedT;

        public override void Initialize()
        {
            var level = LevelsCollector.GetInstance.GetCurrentLevel();
            bezierSpline = level.GetRoadSpline();
            
            normalizedT = 0f;
        }

        public Vector3 GetStartPoint()
        {
            return bezierSpline.GetPoint(0f);
        }
        
        public Vector3 GetCurrentPoint()
        {
            return bezierSpline.GetPoint(normalizedT);
        }
        
        public Vector3 GetEndPoint()
        {
            return bezierSpline.GetPoint(1f);
        }
        
        public Vector3 MovePointAlongSpline(float speed, float deltaTime = 1f)
        {
            var targetPosition = bezierSpline.MoveAlongSpline(ref normalizedT, speed * deltaTime);
            return targetPosition;
        }

        public Quaternion RotateAlongSpline(bool movingForward = true)
        {
            BezierSpline.Segment segment = bezierSpline.GetSegmentAt(normalizedT);
            
            Vector3 forward = movingForward ? segment.GetTangent() : -segment.GetTangent();
            Vector3 upwards = segment.GetNormal();
            
            var targetRotation = Quaternion.LookRotation(forward, upwards);
            return targetRotation;
        }
    }
}