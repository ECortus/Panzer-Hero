using UnityEngine;

namespace PanzerHero.Runtime.Abstract
{
    public abstract class BaseRig : MonoBehaviour, IUnit
    {
        public void Initialize()
        {
            InitializeComponents();
        }

        protected abstract void InitializeComponents();
        
        protected T InitializeComponent<T, TS>() where T : BaseRigComponent<TS>
            where TS : BaseRig
        {
            if (gameObject.TryGetComponent(out T component))
            {
                component.Initialize();
                return component;
            }
            
            var newComponent = gameObject.AddComponent<T>();
            newComponent.Initialize();

            return newComponent;
        }
    }
}