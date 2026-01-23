using System;
using GameDevUtils.Runtime;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Abstract.Base
{
    public interface IUnitHealth
    {
        public bool IsAlive { get; }
        
        public float CurrentHealth { get; }
        public float MaxHealth { get; }
        
        public float CurrentArmor { get; }
        public float MaxArmor { get; }
        
        public void Heal(float amount);
        public void RepairArmor(float amount);
        
        public void Damage(float amount);
    }
    
    public abstract class BaseHealthComponent<T> : BaseRigComponent<T>, IUnitHealth
        where T : BaseRig
    {
        float maxHealth;
        float currentHealth;
        
        Armor armor = new Armor() { Max = 0, Current = 0 };
        
        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;
        
        public float MaxArmor => armor.Max;
        public float CurrentArmor => armor.Current;

        public bool IsAlive => CurrentHealth > 0;
        
        public event Action<float> OnHealthChanged;
        public event Action<float> OnArmorChanged;
        
        public event Action OnDied;
        
        protected void SetHealth(float max)
        {
            this.maxHealth = max;
            currentHealth = maxHealth;
        }
        
        protected void SetArmor(float max)
        {
            armor = new Armor
            {
                Max = max,
                Current = max
            };
        }

        public void Heal(float amount)
        {
            if (amount <= 0)
                return;
            
            currentHealth += amount;
            OnHealthChanged?.Invoke(currentHealth);
            
            ValidateHealth();
        }

        public void RepairArmor(float amount)
        {
            if (amount <= 0)
                return;
            
            armor.Repair(amount);
            OnArmorChanged?.Invoke(armor.Current);
        }

        public void Damage(float amount)
        {
            if (amount <= 0)
                return;

            if (armor.IsAvailable)
            {
                armor.Damage(amount);
                OnArmorChanged?.Invoke(armor.Current);
                
                return;
            }
            
            currentHealth -= amount;
            OnHealthChanged?.Invoke(currentHealth);
            
            ValidateHealth();
        }

        void ValidateHealth()
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            if (currentHealth == 0)
            {
                Destroy();
            }
        }

        protected virtual void Destroy()
        {
            OnDied?.Invoke();
            ObjectHelper.Destroy(gameObject);
        }
        
        class Armor
        {
            public float Current;
            public float Max;
            
            public bool IsAvailable => Current > 0;
            
            public void Repair(float amount)
            {
                Current += amount;
                ValidateArmor();
            }
            
            public void Damage(float amount)
            {
                Current -= amount;
                ValidateArmor();
            }
            
            void ValidateArmor()
            {
                Current = Mathf.Clamp(Current, 0, Max);
            }
        }
    }
}