using System;
using GameDevUtils.Runtime;
using GameDevUtils.Runtime.Extensions;
using PanzerHero.Runtime.Destrictable;
using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units;
using UnityEngine;

namespace PanzerHero.Runtime.Combat
{
    public class BulletBehaviour : MonoBehaviour, ISystemComponent, IManagedComponent
    {
        [SerializeField] private bool hitOwner = false;
        [SerializeField] private float speed = 5f;
        
        [Space(5)]
        [SerializeField] private float damage = 10f;

        bool isDisabled = true;
        
        enum ELaunchType
        {
            InDirection
        }

        ELaunchType launchType;

        BaseRig owner;
        Vector3 direction;

        BulletManager bulletManager;

        Rigidbody rb;
        SphereCollider sphereCollider;
        
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            sphereCollider = GetComponent<SphereCollider>();

            rb.isKinematic = true;
            sphereCollider.isTrigger = true;
        }
        
        public void LaunchInDirection(BaseRig caster, Vector3 dir)
        {
            gameObject.SetActive(false);
            
            launchType = ELaunchType.InDirection;

            owner = caster;
            direction = dir;

            UpdateRotation();

            gameObject.SetActive(true);
            
            isDisabled = false;
            
            OnLaunch();
        }

        void OnLaunch()
        {
            bulletManager = BulletManager.GetInstance;
            bulletManager.Register(this);
        }

        public void UpdateMethod(float deltaTime)
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
            UpdateRotation();
        }
        
        void UpdateRotation()
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        #region Trigger Events

        private void OnTriggerEnter(Collider other)
        {
            if (!hitOwner)
            {
                if (other.transform == owner.transform || other.transform.IsChildOf(owner.transform))
                {
                    return;
                }
            }
            
            if (other.IsSameMask("Player") || other.IsSameMask("Unit"))
            {
                var iunit = other.gameObject.GetComponent<IUnit>();
                OnEnterMethod(iunit);
            }
            
            if (other.IsSameMask("Destrictable"))
            {
                var idestrictable = other.gameObject.GetComponent<IDestrictable>();
                OnEnterMethod(idestrictable);
            }
            
            if (other.IsSameMask("Ground"))
            {
                DestroySelf();
            }
        }

        #endregion
        
        void OnEnterMethod(IUnit obj = null)
        {
            if (obj != null)
            {
                if (obj.IsOpposite(owner))
                {
                    obj.Health.Damage(damage);
                }
            }
            
            DestroySelf();
        }
        
        void OnEnterMethod(IDestrictable obj = null)
        {
            if (obj != null)
            {
                obj.Destroy();
            }
            
            DestroySelf();
        }

        void DestroySelf()
        {
            bulletManager.Unregister(this);
            ObjectHelper.Destroy(gameObject);
        }

        public bool IsDisabled => isDisabled;
    }
}