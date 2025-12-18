using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Systems;
using PanzerHero.Runtime.Units.Player.Data;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player.Components
{
    public class PlayerAttacker : BaseAttackerComponent<PlayerRig>
    {
        PlayerInputEvents inputEvents;
        
        PlayerData data;
        
        public override void Initialize()
        {
            base.Initialize();
            data = Rig.GetData();
            
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
            var prefab = data.rocketPrefab;
            
            var startPoint = transform.position;
            var direction = (targetPoint - startPoint).normalized;
            
            SpawnBullet(prefab, startPoint, direction);
        }
        
        void FireBullet(Vector3 targetPoint)
        {
            var prefab = data.bulletPrefab;
            
            var startPoint = transform.position;
            var direction = (targetPoint - startPoint).normalized;
            
            SpawnBullet(prefab, startPoint, direction);
        }

        protected override void OnDestroy()
        {
            RemoveFireEvents();
        }
    }
}