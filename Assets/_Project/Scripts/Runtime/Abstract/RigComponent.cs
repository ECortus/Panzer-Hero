using UnityEngine;

namespace PanzerHero.Runtime.Units.Components
{
    public abstract class RigComponent<T> : MonoBehaviour
        where T : BaseRig
    {
        protected T Rig { get; private set; }
        
        void Awake()
        {
            Rig = GetComponent<T>();
        }
        
        public virtual void Initialize()
        {
            
        }
        
        protected virtual void OnDestroy()
        {
            
        }
    }
}