using PanzerHero.Runtime.Abstract;
using PanzerHero.Runtime.Systems;
using PanzerHero.Runtime.Units.Player.Data;
using UnityEngine;

namespace PanzerHero.Runtime.Player.Components
{
    public class PlayerAttacker : BaseAttackerComponent<PlayerRig>
    {
        PlayerData data;
        PlayerInputEvents inputEvents;   
        
        public override void Initialize()
        {
            base.Initialize();
            
            var header = GetComponent<PlayerHeader>();
            data = header.GetData();
            
            InitFireEvents();
        }

        void InitFireEvents()
        {
            inputEvents = PlayerInputEvents.GetInstance;
            
            inputEvents.OnMainFireInput += FireRocket;
            inputEvents.OnAdditionalFireInput += FireBullet;
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
    }
}