using PanzerHero.Runtime.Units;
using UnityEngine;

namespace PanzerHero.Runtime.Abstract
{
    public abstract class BaseController<T> : MonoBehaviour
        where T : UnitRig
    {
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
            var rig = gameObject.AddComponent<T>();
            rig.Initialize();
        }
    }
}