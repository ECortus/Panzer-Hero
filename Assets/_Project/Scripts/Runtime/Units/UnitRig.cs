using System;
using PanzerHero.Runtime.Units.Components;
using UnityEngine;

namespace PanzerHero.Runtime.Units
{
    public class UnitRig : MonoBehaviour, IUnit
    {
        UnitHealth unitHealth;
        
        public void Initialize()
        {
            InitializeMandatoryComponents();
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
        
        protected T InitializeComponent<T>() where T : UnitComponent
        {
            if (gameObject.TryGetComponent(out T component))
            {
                return component;
            }
            
            var newComponent = gameObject.AddComponent<T>();
            newComponent.Initialize();

            return newComponent;
        }
    }
}