using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Interfaces;
using PanzerHero.Runtime.Units.Player.Components;
using PanzerHero.Runtime.Units.Player.Data;
using PanzerHero.Runtime.Units.Simultaneous;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player
{
    public class PlayerRig : BaseRig, IPlayer
    {
        PlayerData playerData;
        
        PlayerHealth health;

        IPLayerAttacker attacker;
        IPlayerAmmo ammo;
        
        protected override void InitializeComponents()
        {
            var header = GetComponent<PlayerHeader>();
            playerData = header.GetData();
            
            health = InitializeComponent<PlayerHealth, PlayerRig>();

            InitializeComponent<VehicleEngine, PlayerRig>();
            
            InitializeComponent<PlayerMovement, PlayerRig>();
            InitializeComponent<PlayerBezierSpline, PlayerRig>();
            
            ammo = InitializeComponent<PlayerAmmo, PlayerRig>();
            attacker = InitializeComponent<PlayerAttacker, PlayerRig>();
        }
        
        public PlayerData GetData() => playerData;

        #region Interface
        
        public override Vector3 Position => transform.position;
        
        public override EUnitFaction Faction => EUnitFaction.Player;

        public override IHealth Health => health;
        
        public IPLayerAttacker Attacker => attacker;
        public IPlayerAmmo Ammo => ammo;

        #endregion
    }
}