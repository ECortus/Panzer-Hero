using PanzerHero.Runtime.Units;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Abstract.Base
{
    public abstract class BaseHeader<T> : MonoBehaviour
        where T : BaseRig
    {
        public T Rig { get; private set; }
        
        private void Awake()
        {
            InitializeRig();
        }

        private void Start()
        {
            OnStart();
        }

        protected virtual void OnStart()
        {
            
        }
        
        void InitializeRig()
        {
            Rig = gameObject.AddComponent<T>();
            Rig.Initialize();
        }
    }
}