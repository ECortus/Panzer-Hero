using BezierSolution;
using IsolarvHelperTools.Runtime;
using UnityEngine;

namespace PanzerHero.Runtime.LevelDesign
{
    public interface IRoadSplineData
    {
        float GetFullLenght();
        Vector3 GetStartPoint();
        Vector3 GetEndPoint();
    }
    
    public class RoadSplineController : MonoBehaviour, IRoadSplineData
    {
        BezierSpline bezierSpline;
        
        [Header("Debug")]
        [SerializeField, ReadOnly] float fullLenght;

        public void Initialize()
        {
            GetAllComponents();
            SetFullLenght();
        }
        
        void GetAllComponents()
        {
            bezierSpline = GetComponentInChildren<BezierSpline>();
        }

        void SetFullLenght()
        {
            fullLenght = bezierSpline.GetLengthApproximately(0f, 1f, 60f);
        }
        
        public float GetFullLenght()
        {
            return fullLenght;
        }
        
        public Vector3 GetStartPoint()
        {
            return bezierSpline.GetPoint(0f);
        }
        
        public Vector3 GetEndPoint()
        {
            return bezierSpline.GetPoint(1f);
        }
    }
}