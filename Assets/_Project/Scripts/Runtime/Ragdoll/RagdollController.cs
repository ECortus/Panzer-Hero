using System;
using GameDevUtils.Runtime;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PanzerHero.Runtime.Ragdoll
{
    public class RagdollController : MonoBehaviour
    {
        [Serializable]
        public enum EForceDirection
        {
            Regular, // All random direction and y-positive
            Vertical // Only y-positive direction
        }

        [SerializeField] private bool isToSwitchObject = true;
        [SerializeField, DrawIf("isToSwitchObject", true)] private GameObject regularObject;
        [SerializeField, DrawIf("isToSwitchObject", true)] private GameObject ragdollObject;
        
        [Space(5)]
        [SerializeField] private float minWeightOfPart = 1f;
        [SerializeField] private float force = 1000f;
        [SerializeField] private float angularSpeed = 90;
        [SerializeField] private EForceDirection forceDirection = EForceDirection.Regular;
        
        bool partsWritten = false;
        
        Rigidbody[] parts;
        
        Collider[] colliders;
        Vector3[] partsStartPositions;
        Quaternion[] partsStartRotations;

        private void Awake()
        {
            WritePartsData();
            SetAsRegular();
        }

        void WritePartsData()
        {
            if (partsWritten)
            {
                return;
            }
            
            partsWritten = true;
            
            parts = ragdollObject.GetComponentsInChildren<Rigidbody>();
            var count = parts.Length;
            
            colliders = new Collider[count];
            partsStartPositions = new Vector3[count];
            partsStartRotations = new Quaternion[count];
            
            for (int i = 0; i < count; i++)
            {
                colliders[i] = parts[i].GetComponent<Collider>();
                partsStartPositions[i] = parts[i].position;
                partsStartRotations[i] = parts[i].rotation;
            }
        }

        public void SetAsRegular()
        {
            if (isToSwitchObject)
            {
                regularObject.SetActive(true);
                ragdollObject.SetActive(false);
            }

            ResetParts();
        }
        
        public void SetAsRagdoll()
        {
            if (isToSwitchObject)
            {
                regularObject.SetActive(false);
                ragdollObject.SetActive(true);
            }

            ForceAllParts();
        }

        void ResetParts()
        {
            if (!partsWritten)
            {
                WritePartsData();
            }
            
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].isKinematic = true;
                colliders[i].enabled = false;
                
                parts[i].position = partsStartPositions[i];
                parts[i].rotation = partsStartRotations[i];
            }
        }

        void ForceAllParts()
        {
            if (!partsWritten)
            {
                WritePartsData();
            }
            
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].isKinematic = false;
                colliders[i].enabled = true;
                
                ForcePart(parts[i], force, angularSpeed);
            }
        }

        void ForcePart(Rigidbody part, float forceValue, float angularSpeedValue, ForceMode mode = ForceMode.Force)
        {
            part.isKinematic = false;
            part.mass = Mathf.Max(part.mass, minWeightOfPart);
            
            var direction = GetForceDirection();
            part.AddForce(direction * forceValue, mode);
                
            var angularVelocity = GetAngularDirection() * angularSpeedValue;
            part.angularVelocity = angularVelocity;
        }

        Vector3 GetForceDirection()
        {
            Vector3 direction = new Vector3();

            switch (forceDirection)
            {
                case EForceDirection.Regular:
                    direction.y = Random.Range(0f, 1f);
                    break;
                case EForceDirection.Vertical:
                    direction = Random.insideUnitSphere;
                    direction.y = Mathf.Abs(direction.y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return direction;
        }
        
        Vector3 GetAngularDirection()
        {
            return Random.insideUnitSphere;
        }
    }
}