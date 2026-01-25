using System;
using PanzerHero.Runtime.Units.Interfaces;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Abstract.Base
{
    public abstract class BaseRig : MonoBehaviour, IUnit
    {
        bool isDisabled = false;
        UnitsManager unitsManager;

        event Action InitializationComponentsInvoke;
        
        public void Initialize()
        {
            InitializeComponents();
            InitializationComponentsInvoke?.Invoke();

            unitsManager = UnitsManager.GetInstance;
            unitsManager.Register(this);

            isDisabled = false;
        }

        protected abstract void InitializeComponents();
        
        protected T InitializeComponent<T, TS>() where T : BaseRigComponent<TS>
            where TS : BaseRig
        {
            if (!gameObject.TryGetComponent(out T component))
            {
                component = gameObject.AddComponent<T>();
            }
            
            InitializationComponentsInvoke += () => component.Initialize();
            return component;
        }
        
        protected virtual void OnDestroy()
        {
            isDisabled = true;
            
            InitializationComponentsInvoke = null;
            unitsManager?.Unregister(this);
        }
        
        #region Interface

        public abstract Vector3 Position { get; }
        
        public abstract EUnitFaction Faction { get; }

        public abstract IHealth Health { get; }

        public bool IsAlive => Health.IsAlive;

        public bool IsDisabled => isDisabled;

        public bool IsFriendly(IUnit other)
        {
            return Faction == other.Faction;
        }
        
        public bool IsOpposite(IUnit other)
        {
            return Faction != other.Faction;
        }

        #endregion
    }
}