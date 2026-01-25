using Cysharp.Threading.Tasks;
using GameDevUtils.Runtime;
using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Player.Data;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player.Components
{
    public interface IPlayerAmmo
    {
        IAmmo Rockets { get; }
        IAmmo Bullets { get; }
    }
    
    public interface IAmmo
    {
        int Amount { get; }
                
        bool IsReloading { get; }
        float ReloadingProgress { get; }
                
        void Reduce();
        void Reload();
    }
    
    public class PlayerAmmo : BaseRigComponent<PlayerRig>, IPlayerAmmo
    {
        PlayerData data;
        
        Ammo rocketAmmo;
        Ammo bulletsAmmo;

        public override void Initialize()
        {
            base.Initialize();

            data = Rig.GetData();

            rocketAmmo = new Ammo(data.rocketsAmmoAmount, data.rocketsReloadTime);
            bulletsAmmo = new Ammo(data.bulletsAmmoAmount, data.bulletsReloadTime);
        }

        class Ammo : IAmmo
        {
            int ammo;
            readonly int maxAmmo;

            bool isReloading;
            float reloadingProgress;

            readonly float reloadTime;

            public Ammo(int maximumAmmo, float reloadingTime)
            {
                maxAmmo = maximumAmmo;
                reloadTime = reloadingTime;
                
                ForcedReloading();
            }

            public int Amount => ammo;

            public void Reduce()
            {
                ammo -= 1;
                if (ammo == 0)
                {
                    Reload();
                }
            }

            public void Reload()
            {
                if (isReloading)
                {
                    return;
                }
                
                AsyncTaskHelper.CreateTask(async () =>
                {
                    isReloading = true;
                    await ReloadingTask();
                    isReloading = false;
                });
            }

            public bool IsReloading => isReloading;
            public float ReloadingProgress => isReloading ? reloadingProgress : 1f;

            async UniTask ReloadingTask()
            {
                float startTime = Time.time;
                float waitTime = reloadTime;
                
                while (true)
                {
                    float currentTime = Time.time;
                    float difference = currentTime - startTime;
                    
                    if (difference >= waitTime)
                    {
                        reloadingProgress = 1f;
                        break;
                    }

                    reloadingProgress = difference / waitTime;
                    await UniTask.Yield();
                }

                ammo = maxAmmo;
            }

            void ForcedReloading()
            {
                ammo = maxAmmo;
                isReloading = false;
            }
        }

        #region Interface

        public IAmmo Rockets => rocketAmmo;
        public IAmmo Bullets => bulletsAmmo;

        #endregion
    }
}