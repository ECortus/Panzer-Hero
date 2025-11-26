using System;
using BezierSolution;
using PanzerHero.Runtime.LevelDesign.Levels;
using PanzerHero.Runtime.Units.Components;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player
{
    public class PlayerBezierSpline : RigComponent
    {
        [SerializeField] BezierSpline bezierSpline;
        [SerializeField] Vector3[] positionsCache;
        
        [SerializeField] int currentSegmentId;
        [SerializeField] Segment[] segmentsCache;

        Segment currentSegment;
        
        public bool PathIsFinished { get; private set; }

        [Serializable]
        class Segment
        {
            public Transform transform;
            
            public int id;
            public Vector3 previousPosition;
            public Vector3 nextPosition;

            public Vector3 GetDirectionOfSegment()
            {
                return (nextPosition - previousPosition).normalized;
            }
            
            public float GetDistanceToPreviousPoint()
            {
                return (previousPosition - transform.position).sqrMagnitude;
            }
            
            public float GetDistanceToNextPoint()
            {
                return (nextPosition - transform.position).sqrMagnitude;
            }

            public float GetLenght()
            {
                return (previousPosition - nextPosition).sqrMagnitude;
            }
        }

        public override void Initialize()
        {
            var level = LevelsCollector.GetInstance.GetCurrentLevel();
            bezierSpline = level.GetRoadSpline();
            
            InitCache();
        }

        void InitCache()
        {
            positionsCache = bezierSpline.pointCache.positions;
            segmentsCache = new Segment[positionsCache.Length - 1];
            
            for (int i = 0; i < positionsCache.Length - 1; i++)
            {
                segmentsCache[i] = new Segment()
                {
                    transform = transform,
                    id = i,
                    previousPosition = positionsCache[i],
                    nextPosition = positionsCache[i + 1]
                };
            }
            
            SetSegmentId(0);
        }

        public Vector3 GetStartPoint()
        {
            return positionsCache[0];
        }
        
        public Vector3 GetDirection()
        {
            return currentSegment.GetDirectionOfSegment();
        }
        
        public Vector3 GetPoint(int i)
        {
            return positionsCache[i];
        }
        
        public Vector3 GetEndPoint()
        {
            return positionsCache[^1];
        }

        void Update()
        {
            if (currentSegment == null)
            {
                return;
            }

            if (PathIsFinished)
            {
                return;
            }
            
            var segmentLenght = currentSegment.GetLenght();
            var distanceToPreviousPoint = currentSegment.GetDistanceToPreviousPoint();
            var distanceToNextPoint = currentSegment.GetDistanceToNextPoint();

            if (distanceToNextPoint > segmentLenght || distanceToPreviousPoint > segmentLenght)
            {
                if (distanceToPreviousPoint < distanceToNextPoint)
                {
                    SetSegmentIdLower();
                    return;
                }
                
                if (distanceToNextPoint < distanceToPreviousPoint)
                {
                    if (IsFinishSegment())
                    {
                        PathIsFinished = true;
                        return;
                    }
                    
                    SetSegmentIdHigher();
                    return;
                }
            }
        }

        bool IsFinishSegment()
        {
            return currentSegmentId + 1 == positionsCache.Length - 1;
        }

        void SetSegmentIdLower()
        {
            SetSegmentId(currentSegmentId - 1);
        }

        void SetSegmentIdHigher()
        {
            SetSegmentId(currentSegmentId + 1);
        }

        void SetSegmentId(int id)
        {
            id = Mathf.Clamp(id, 0, positionsCache.Length - 2);
            
            currentSegmentId = id;
            currentSegment = segmentsCache[id];
        }
    }
}