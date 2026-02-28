using UnityEngine;
using UnityEngine.Events;

namespace PanzerHero.Runtime.Destrictable
{
    public class DestrictableObject : MonoBehaviour, IDestrictable
    {
        protected virtual void Awake()
        {
            
        }
        
        void Enable()
        {
            gameObject.SetActive(true);
        }

        void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Renew()
        {
            Enable();
        }
        
        public void Destroy()
        {
            DestroyProcess();
        }

        protected virtual void RenewProcess()
        {
            Enable();
        }

        protected virtual void DestroyProcess()
        {
            Disable();
        }
    }
}