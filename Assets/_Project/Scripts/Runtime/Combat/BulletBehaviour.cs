using System;
using GameDevUtils.Runtime;
using GameDevUtils.Runtime.Extensions;
using PanzerHero.Runtime.Abstract;
using UnityEngine;

namespace PanzerHero.Runtime.Combat
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        
        enum ELaunchType
        {
            InDirection
        }

        ELaunchType launchType;
        
        Vector3 direction;

        Rigidbody rb;
        SphereCollider sphereCollider;
        
        public void LaunchInDirection(Vector3 dir)
        {
            launchType = ELaunchType.InDirection;
            direction = dir;
        }

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            sphereCollider = GetComponent<SphereCollider>();

            rb.isKinematic = true;
            sphereCollider.isTrigger = true;
        }

        void Update()
        {
            UpdateMethod(Time.deltaTime);
        }

        void UpdateMethod(float deltaTime)
        {
            if (launchType == ELaunchType.InDirection)
            {
                UpdateInDirection(deltaTime);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        void UpdateInDirection(float deltaTime)
        {
            var move = direction * speed * deltaTime;
            var newPosition = transform.position + move;

            transform.position = newPosition;
            transform.rotation = Quaternion.LookRotation(direction);
        }

        #region Trigger Events

        private void OnTriggerEnter(Collider other)
        {
            if (other.IsSameMask("Player") || other.IsSameMask("Unit"))
            {
                var iunit = other.gameObject.GetComponent<IUnit>();
                OnEnterMethod(iunit);
            }
            
            if (other.IsSameMask("Ground"))
            {
                OnEnterMethod();
            }
        }

        #endregion
        
        void OnEnterMethod(IUnit obj = null)
        {
            if (obj != null)
            {
                
            }
            
            DestroySelf();
        }

        void DestroySelf()
        {
            ObjectHelper.Destroy(gameObject);
        }
    }
}