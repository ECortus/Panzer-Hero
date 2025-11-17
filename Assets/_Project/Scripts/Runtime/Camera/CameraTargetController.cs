using UnityEngine;
using Unity.Cinemachine;
using UnityEditor;

namespace PanzerHero.Runtime
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class CameraTargetController : MonoBehaviour
    {
        [SerializeField] private Transform overrideTarget;
        
        void Start()
        {
            SetCinemachineTarget();
        }

        void SetCinemachineTarget()
        {
            var cinemachine = FindAnyObjectByType<CinemachineCamera>();
            cinemachine.Follow = overrideTarget ? overrideTarget : transform;
        }
    }
}