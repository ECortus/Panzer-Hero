using GameDevUtils.Runtime.UI.Abstract;
using PanzerHero.Runtime.Units;
using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Interfaces;
using PanzerHero.Runtime.Units.Player.Components;
using UnityEngine;
using UnityEngine.UI;

namespace PanzerHero.UI.Gameplay.HUD
{
    public abstract class UAmmoCounter : UDynamicFloatField
    {
        protected abstract IAmmo Ammo { get; }
        protected abstract ITimerInfo TimerInfo { get; }
        
        UnitsManager unitsManager;

        [Space(5)]
        [SerializeField] Image reloadImage;
        [SerializeField] Slider timerSlider;

        IPlayer _player;
        protected IPlayer player
        {
            get
            {
                if (_player == null)
                {
                    _player = unitsManager.Player;
                }

                return _player;
            }
        }
        
        bool isReloading = false;
        
        protected override void OnStart()
        {
            unitsManager = UnitsManager.GetInstance;
            base.OnStart();
            
            isReloading = false;
            reloadImage.gameObject.SetActive(false);
        }
        
        protected override float GetTargetValue()
        {
            if (Ammo == null)
            {
                return -1;
            }

            return Ammo.Amount;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            UpdateAdditionalInfo();
        }

        void UpdateAdditionalInfo()
        {
            if (Ammo != null)
            {
                if (Ammo.IsReloading != isReloading)
                {
                    isReloading = Ammo.IsReloading;
                    reloadImage.gameObject.SetActive(isReloading);
                    timerSlider.gameObject.SetActive(!isReloading);
                }

                if (isReloading)
                {
                    reloadImage.fillAmount = Ammo.ReloadingProgress;
                }
            }
            
            if (TimerInfo != null)
            {
                timerSlider.value = TimerInfo.DelayProgress;
            }
        }
    }
}