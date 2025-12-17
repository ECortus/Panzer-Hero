using System;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Abstract.Base
{
    public abstract class BaseRig : MonoBehaviour, IUnit
    {
        UnitsManager unitsManager;

        event Action InitializationComponentsInvoke;
        
        public void Initialize()
        {
            InitializeComponents();
            InitializationComponentsInvoke?.Invoke();

            unitsManager = UnitsManager.GetInstance;
            unitsManager.Register(this);
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
            InitializationComponentsInvoke = null;
            unitsManager?.Unregister(this);
        }
        
        #region Interface

        public abstract EUnitFaction Faction { get; }
        public abstract bool IsPlayer { get; }

        public abstract IUnitHealth Health { get; }

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