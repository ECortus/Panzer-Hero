using UnityEngine;
using Unity.Cinemachine;
using UnityEditor;

namespace PanzerHero.Runtime
{
    [ExecuteAlways]
    public class CameraTargetController : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        
        [Space(5)] 
        [SerializeField] private float distanceToTarget = 10;
        [SerializeField] private float heightOffset = 5f;
        
        CinemachineCamera cinemachine;
        
        void Start()
        {
            Init();
        }

        void Init()
        {
            if (targetTransform == null)
                return;
            
            SetPosition();
            SetCinemachineTarget();
        }

        void SetPosition()
        {
            var targetPosition = targetTransform.position;

            var direction = transform.rotation * -Vector3.forward;
            targetPosition += direction * distanceToTarget;
            targetPosition.y += heightOffset;
            
            transform.position = targetPosition;
        }

        void SetCinemachineTarget()
        {
            cinemachine = FindAnyObjectByType<CinemachineCamera>();
            cinemachine.Follow = transform;
            ForceMoveToPosition();
        }

        void Update()
        {
            if (!targetTransform)
                return;
            
            SetPosition();
        }

        void ForceMoveToPosition()
        {
            cinemachine.ForceCameraPosition(transform.position, transform.rotation);
        }
    }
}