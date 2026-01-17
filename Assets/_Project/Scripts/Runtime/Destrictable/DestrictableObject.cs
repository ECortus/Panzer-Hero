using UnityEngine;
using UnityEngine.Events;

namespace PanzerHero.Runtime.Destrictable
{
    public class DestrictableObject : MonoBehaviour, IDestrictable
    {
        public bool disableObject = true;
        public UnityEvent OnDestroy;

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