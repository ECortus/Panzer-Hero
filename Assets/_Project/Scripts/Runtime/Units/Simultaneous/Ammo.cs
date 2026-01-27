using Cysharp.Threading.Tasks;
using GameDevUtils.Runtime;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Simultaneous
{
    public interface IAmmo
    {
        int Amount { get; }
                
        bool IsReloading { get; }
        float ReloadingProgress { get; }
                
        void Reduce();
        void Reload();
    }
    
    public class Ammo : IAmmo
    {
        int ammo;
        readonly int maxAmmo;

        bool isReloading;
        float reloadingProgress;

        float reloadDuration;

        public Ammo(int maximumAmmo, float reloadingDuration)
        {
            maxAmmo = maximumAmmo;
            reloadDuration = reloadingDuration;
            
            ForcedReloading();
        }

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

        public void SetReloadDuration(float duration)
        {
            reloadDuration = duration;
        }

        async UniTask ReloadingTask()
        {
            float startTime = Time.time;
            while (true)
            {
                float currentTime = Time.time;
                float difference = currentTime - startTime;
                
                if (difference >= reloadDuration)
                {
                    reloadingProgress = 1f;
                    break;
                }

                reloadingProgress = difference / reloadDuration;
                await UniTask.Yield();
            }

            ammo = maxAmmo;
        }

        void ForcedReloading()
        {
            ammo = maxAmmo;
            isReloading = false;
        }

        #region Interface

        public int Amount => ammo;
        public bool IsReloading => isReloading;
        public float ReloadingProgress => isReloading ? reloadingProgress : 1f;

        #endregion
    }
}