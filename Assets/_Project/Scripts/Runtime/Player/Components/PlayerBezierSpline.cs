using System;
using BezierSolution;
using PanzerHero.Runtime.LevelDesign;
using PanzerHero.Runtime.LevelDesign.Levels;
using PanzerHero.Runtime.Units.Components;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player
{
    public class PlayerBezierSpline : RigComponent<PlayerRig>
    {
        [SerializeField] BezierSpline bezierSpline;
        [SerializeField] Vector3[] positionsCache;
        
        [SerializeField] int currentSegmentId;
        [SerializeField] Segment[] segmentsCache;

        Segment currentSegment;
        
        public bool CanCalculate() => currentSegment != null;

        [Serializable]
        class Segment
        {
            public Transform transform;
            
            public int id;
            public Vector3 previousPosition;
            public Vector3 nextPosition;
            
            public bool IsPointOnSegment(Vector3 point)
            {
                return IsPointOnSegment(point, out _, out _);
            }

            public bool IsPointOutOfSegment(Vector3 point, out bool isOutBehind, out bool isOutFront)
            {
                return !IsPointOnSegment(point, out isOutBehind, out isOutFront);
            }

            public bool IsPointOnSegment(Vector3 point, out bool isOutBehind, out bool isOutFront)
            {
                var segmentLenght = GetLenght();
                
                var distanceToPrevious = (point - previousPosition).sqrMagnitude;
                var distanceToNext = (point - nextPosition).sqrMagnitude;

                var pointIsBehind = distanceToNext > segmentLenght;
                var pointIsFront = distanceToPrevious > segmentLenght;
                
                isOutBehind = pointIsBehind;
                isOutFront = pointIsFront;
                
                return !pointIsFront && !pointIsBehind;
            }

            public Vector3 GetDirectionOfSegment()
            {
                return (nextPosition - previousPosition).normalized;
            }
            
            public Vector3 GetDirectionToNext()
            {
                var to = nextPosition + GetDirectionOfSegment() * 1.5f;
                var from = transform.position;
                return (to - from).normalized;
            }
            
            public Vector3 GetDirectionToPrevious()
            {
                var to = previousPosition - GetDirectionOfSegment() * 1.5f;
                var from = transform.position;
                return (to - from).normalized;
            }

            public float GetLenght(float mod = 1f)
            {
                return (previousPosition - nextPosition).sqrMagnitude * mod;
            }
        }

        public override void Initialize()
        {
            var levelManager = LevelManager.GetInstance;
            levelManager.OnLevelChanged += UpdateSpline;
        }

        public void UpdateSpline()
        {
            var manager = LevelManager.GetInstance;
            var data = manager.GetLevelData();
            bezierSpline = data.RoadSpline;
            
            InitCache();
            TeleportToStartPoint();
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
        
        void TeleportToStartPoint()
        {
            var start = GetStartPoint();
            Rig.Movement.TeleportToPoint(start);
        }

        public Vector3 GetStartPoint()
        {
            return positionsCache[0];
        }

        public Vector3 GetDirectionOfSegment()
        {
            return currentSegment.GetDirectionOfSegment();
        }
        
        public Vector3 GetDirectionToNext()
        {
            return currentSegment.GetDirectionToNext();
        }
        
        public Vector3 GetDirectionToPrevious()
        {
            return currentSegment.GetDirectionToPrevious();
        }
        
        public void SetCurrentSegmentId(int id)
        {
            SetSegmentId(id);
        }
        
        public Vector3 GetPoint(int i)
        {
            return positionsCache[i];
        }
        
        public Vector3 GetEndPoint()
        {
            return positionsCache[^1];
        }
        
        public bool IsPathNotStarted()
        {
            var player = transform.position;
            var startPosition = segmentsCache[0].previousPosition;

            return (player - startPosition).sqrMagnitude < 2f;
        }
        
        public bool IsPathFinished()
        {
            var player = transform.position;
            var startPosition = segmentsCache[^1].nextPosition;

            return (player - startPosition).sqrMagnitude < 2f;
        }

        public bool IsPointOnSegment(Vector3 point, out int segmentId)
        {
            for (int i = 0; i < segmentsCache.Length; i++)
            {
                var segment = segmentsCache[i];
                if (segment.IsPointOnSegment(point))
                {
                    segmentId = i;
                    return true;
                }
            }
            
            segmentId = -1;
            return false;
        }

        void Update()
        {
            if (currentSegment == null)
            {
                return;
            }

            if (currentSegment.IsPointOutOfSegment(transform.position, out bool isOutBehind, out bool isOutFront))
            {
                if (isOutBehind)
                {
                    SetSegmentIdLower();
                    return;
                }
                
                if (isOutFront)
                {
                    if (IsFinishSegment())
                    {
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