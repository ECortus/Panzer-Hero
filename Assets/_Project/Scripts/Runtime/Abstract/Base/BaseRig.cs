using System;
using PanzerHero.Runtime.Units.Components;
using UnityEngine;

namespace PanzerHero.Runtime.Units
{
    public abstract class BaseRig : MonoBehaviour, IUnit
    {
        public void Initialize()
        {
            InitializeComponents();
        }

        protected virtual void InitializeComponents()
        {
            
        }
        
        protected T InitializeComponent<T, TS>() where T : RigComponent<TS>
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