using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Systems;
using PanzerHero.Runtime.Units.Player.Data;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player.Components
{
    public class PlayerAttacker : BaseAttackerComponent<PlayerRig>
    {
        PlayerInputEvents inputEvents;

        PlayerPointersCollection _pointersCollection;
        
        PlayerData data;
        
        public override void Initialize()
        {
            base.Initialize();
            data = Rig.GetData();
            
            _pointersCollection = GetComponentInChildren<PlayerPointersCollection>();
            
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
            var point = _pointersCollection.mainFirePoint;
            
            var prefab = data.rocketPrefab;
            
            var startPoint = point.position;
            var direction = (targetPoint - startPoint).normalized;
            
            SpawnBullet(prefab, startPoint, direction);
        }
        
        void FireBullet(Vector3 targetPoint)
        {
            var point = _pointersCollection.additionalFirePoint;
            
            var prefab = data.bulletPrefab;
            
            var startPoint = point.position;
            var direction = (targetPoint - startPoint).normalized;
            
            SpawnBullet(prefab, startPoint, direction);
        }

        protected override void OnDestroy()
        {
            RemoveFireEvents();
        }
    }
}