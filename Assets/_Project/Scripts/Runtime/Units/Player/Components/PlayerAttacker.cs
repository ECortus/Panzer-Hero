using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Systems;
using PanzerHero.Runtime.Units.Player.Data;
using PanzerHero.Runtime.Units.Simultaneous;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player.Components
{
    public interface IPLayerAttacker
    {
        ITimerInfo MainFireTimerInfo { get; }
        ITimerInfo AlternativeFireTimerInfo { get; }
    }
    
    public class PlayerAttacker : BaseTimedAttackerComponent<PlayerRig>, IPLayerAttacker
    {
        PlayerInputEvents inputEvents;

        TankPointersCollection pointersCollection;
        
        PlayerData data;

        IPlayerAmmo ammo;
        IUpgradedCharacter damageCharacter;

        Timer mainFireTimer;
        Timer alternativeFireTimer;
        
        public override void Initialize()
        {
            base.Initialize();
            data = Rig.GetData();

            ammo = GetComponent<IPlayerAmmo>();
            
            var upgradedCharacters = GetComponent<IPlayerUpgradedCharacters>();
            damageCharacter = upgradedCharacters.Damage;
            
            pointersCollection = GetComponentInChildren<TankPointersCollection>();

            mainFireTimer = CreateNewTimer(data.mainFireDelayAttack);
            alternativeFireTimer = CreateNewTimer(data.alternativeFireDelayAttack);
            
            InitFireEvents();
        }

        void InitFireEvents()
        {
            inputEvents = PlayerInputEvents.GetInstance;
            
            inputEvents.OnMainFireInput += TryFireRocket;
            inputEvents.OnAlternativeFireInput += TryFireBullet;
        }

        void RemoveFireEvents()
        {
            inputEvents.OnMainFireInput -= TryFireRocket;
            inputEvents.OnAlternativeFireInput -= TryFireBullet;
        }

        void TryFireRocket(Vector3 targetPoint)
        {
            var rockets = ammo.Rockets;
            if (rockets.IsReloading)
            {
                return;
            }
            
            if (!mainFireTimer.CanDoAction())
            {
                return;
            }
            
            FireRocket(targetPoint);
            
            rockets.Reduce();
            mainFireTimer.Reset();
        }
        
        void TryFireBullet(Vector3 targetPoint)
        {
            var bullets = ammo.Bullets;
            if (bullets.IsReloading)
            {
                return;
            }
            
            if (!alternativeFireTimer.CanDoAction())
            {
                return;
            }
            
            FireBullet(targetPoint);
            
            bullets.Reduce();
            alternativeFireTimer.Reset();
        }
        
        void FireRocket(Vector3 targetPoint)
        {
            var point = pointersCollection.mainFirePoint;
            
            var prefab = data.rocketPrefab;
            
            var startPoint = point.position;
            var direction = (targetPoint - startPoint).normalized;
            
            var damage = GetDamage(data.mainFireDamage);
            SpawnBulletWithCustomDamage(prefab, damage, startPoint, direction);
        }
        
        void FireBullet(Vector3 targetPoint)
        {
            var point = pointersCollection.alternativeFirePoint;
            
            var prefab = data.bulletPrefab;
            
            var startPoint = point.position;
            var direction = (targetPoint - startPoint).normalized;

            var damage = GetDamage(data.alternativeFireDamage);
            SpawnBulletWithCustomDamage(prefab, damage, startPoint, direction);
        }

        float GetDamage(float defaultDamage)
        {
            float mod = damageCharacter.CurrentProgressValue;
            return defaultDamage * mod;
        }

        protected override void OnDestroy()
        {
            RemoveFireEvents();
        }

        #region Interface

        public ITimerInfo MainFireTimerInfo => mainFireTimer;
        public ITimerInfo AlternativeFireTimerInfo => alternativeFireTimer;

        #endregion
    }
}