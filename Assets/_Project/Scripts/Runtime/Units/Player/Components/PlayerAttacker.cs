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

        private Timer mainFireTimer;
        private Timer additionalFireTimer;
        
        public override void Initialize()
        {
            base.Initialize();
            data = Rig.GetData();
            
            _pointersCollection = GetComponentInChildren<PlayerPointersCollection>();

            mainFireTimer = CreateNewTimer(data.mainFireDelayAttack);
            additionalFireTimer = CreateNewTimer(data.addditionalFireDelayAttack);
            
            InitFireEvents();
        }

        void InitFireEvents()
        {
            inputEvents = PlayerInputEvents.GetInstance;
            
            inputEvents.OnMainFireInput += FireRocket;
            inputEvents.OnAdditionalFireInput += FireBullet;
        }

        void RemoveFireEvents()
        {
            inputEvents.OnMainFireInput -= FireRocket;
            inputEvents.OnAdditionalFireInput -= FireBullet;
        }

        void FireRocket(Vector3 targetPoint)
        {
            if (!mainFireTimer.CanDoAction())
            {
                return;
            }
            
            var point = _pointersCollection.mainFirePoint;
            
            var prefab = data.rocketPrefab;
            
            var startPoint = point.position;
            var direction = (targetPoint - startPoint).normalized;
            
            SpawnBullet(prefab, startPoint, direction);
            
            mainFireTimer.Reset();
        }
        
        void FireBullet(Vector3 targetPoint)
        {
            if (!additionalFireTimer.CanDoAction())
            {
                return;
            }
            
            var point = _pointersCollection.additionalFirePoint;
            
            var prefab = data.bulletPrefab;
            
            var startPoint = point.position;
            var direction = (targetPoint - startPoint).normalized;
            
            SpawnBullet(prefab, startPoint, direction);
            
            additionalFireTimer.Reset();
        }

        protected override void OnDestroy()
        {
            RemoveFireEvents();
        }
    }
}