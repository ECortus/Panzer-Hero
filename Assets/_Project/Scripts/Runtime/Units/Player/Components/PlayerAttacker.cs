using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Systems;
using PanzerHero.Runtime.Units.Player.Data;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player.Components
{
    public class PlayerAttacker : BaseTimedAttackerComponent<PlayerRig>
    {
        PlayerInputEvents inputEvents;

        PlayerPointersCollection _pointersCollection;
        
        PlayerData data;

        Timer mainFireTimer;
        Timer alternativeFireTimer;
        
        public override void Initialize()
        {
            base.Initialize();
            data = Rig.GetData();
            
            _pointersCollection = GetComponentInChildren<PlayerPointersCollection>();

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
            if (!mainFireTimer.CanDoAction())
            {
                return;
            }
            
            FireRocket(targetPoint);
            
            mainFireTimer.Reset();
        }
        
        void TryFireBullet(Vector3 targetPoint)
        {
            if (!alternativeFireTimer.CanDoAction())
            {
                return;
            }
            
            FireBullet(targetPoint);
            
            alternativeFireTimer.Reset();
        }
        
        void FireRocket(Vector3 targetPoint)
        {
            var point = _pointersCollection.mainFirePoint;
            
            var prefab = data.rocketPrefab;
            
            var startPoint = point.position;
            var direction = (targetPoint - startPoint).normalized;

            var damage = data.mainFireDamage;
            
            SpawnBulletWithCustomDamage(prefab, damage, startPoint, direction);
        }
        
        void FireBullet(Vector3 targetPoint)
        {
            var point = _pointersCollection.alternativeFirePoint;
            
            var prefab = data.bulletPrefab;
            
            var startPoint = point.position;
            var direction = (targetPoint - startPoint).normalized;

            var damage = data.alternativeFireDamage;
            
            SpawnBulletWithCustomDamage(prefab, damage, startPoint, direction);
        }

        protected override void OnDestroy()
        {
            RemoveFireEvents();
        }
    }
}