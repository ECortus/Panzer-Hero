using UnityEngine;
using UnityEngine.Events;

namespace PanzerHero.Runtime.Destrictable
{
    public class DestrictableObject : MonoBehaviour, IDestrictable
    {
        public bool disableObject = true;
        public UnityEvent OnDestroy;

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
        
        public virtual void Destroy()
        {
            DestroyProcess();
        }

        void DestroyProcess()
        {
            OnDestroy?.Invoke();
            
            if (disableObject)
            {
                Disable();
            }
        }
    }
}