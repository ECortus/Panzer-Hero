using System;
using PanzerHero.Runtime.Units.Components;
using UnityEngine;

namespace PanzerHero.Runtime.Units
{
    public abstract class BaseRig : MonoBehaviour, IUnit
    {
        UnitHealth unitHealth;
        
        public void Initialize()
        {
            InitializeMandatoryComponents();
            InitializeAdditionalComponents();
        }
        
        void InitializeMandatoryComponents()
        {
            InitializeHealth();
        }

        protected virtual void InitializeAdditionalComponents()
        {
            
        }
        
        void InitializeHealth()
        {
            InitializeComponent<UnitHealth>();
        }
        
        protected T InitializeComponent<T>() where T : RigComponent
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