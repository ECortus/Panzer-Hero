using GameDevUtils.Runtime;
using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Player.Data;
using PanzerHero.Runtime.Units.Simultaneous;

namespace PanzerHero.Runtime.Units.Player.Components
{
    public interface IPlayerAmmo
    {
        IAmmo Rockets { get; }
        IAmmo Bullets { get; }
    }
    
    public class PlayerAmmo : BaseRigComponent<PlayerRig>, IPlayerAmmo
    {
        PlayerData data;

        IUpgradedCharacter reloadDurationCharacter;
        
        Ammo rocketAmmo;
        Ammo bulletsAmmo;

        public override void Initialize()
        {
            base.Initialize();

            data = Rig.GetData();

            var characters = GetComponent<IPlayerUpgradedCharacters>();
            reloadDurationCharacter = characters.ReloadDuration;
            
            var rocketReloadTime = GetReloadDuration(data.rocketsReloadTime);
            var bulletReloadTime = GetReloadDuration(data.bulletsReloadTime);

            rocketAmmo = new Ammo(data.rocketsAmmoAmount, rocketReloadTime);
            bulletsAmmo = new Ammo(data.bulletsAmmoAmount, bulletReloadTime);

            reloadDurationCharacter.OnChanged += SetReloadDurationToAmmo;
        }

        void SetReloadDurationToAmmo()
        {
            var rocketReloadTime = GetReloadDuration(data.rocketsReloadTime);
            var bulletReloadTime = GetReloadDuration(data.bulletsReloadTime);
            
            rocketAmmo.SetReloadDuration(rocketReloadTime);
            bulletsAmmo.SetReloadDuration(bulletReloadTime);
        }

        float GetReloadDuration(float defaultDuration)
        {
            var mod = reloadDurationCharacter.CurrentProgressValue;
            return defaultDuration * mod;
        }

        #region Interface

        public IAmmo Rockets => rocketAmmo;
        public IAmmo Bullets => bulletsAmmo;

        #endregion
    }
}