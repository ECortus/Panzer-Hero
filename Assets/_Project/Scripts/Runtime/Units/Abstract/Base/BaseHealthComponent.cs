using System;
using GameDevUtils.Runtime;
using UnityEngine;

namespace PanzerHero.Runtime.Abstract
{
    public abstract class BaseHealthComponent<T> : BaseRigComponent<T> 
        where T : BaseRig
    {
        float maxHealth;
        float currentHealth;
        
        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;
        
        public event Action<float> OnHealthChanged;
        public event Action OnDied;

        public void Heal(float amount)
        {
            if (amount <= 0)
                return;
            
            currentHealth += amount;
            OnHealthChanged?.Invoke(currentHealth);
            
            ValidateHealth();
        }

        public void Damage(float amount)
        {
            if (amount <= 0)
                return;
            
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
    }
}