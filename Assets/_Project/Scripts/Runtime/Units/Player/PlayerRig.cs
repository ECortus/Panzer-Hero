using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Player.Components;
using PanzerHero.Runtime.Units.Player.Data;

namespace PanzerHero.Runtime.Units.Player
{
    public class PlayerRig : BaseRig
    {
        PlayerData playerData;
        PlayerHealth health;
        
        protected override void InitializeComponents()
        {
            var header = GetComponent<PlayerHeader>();
            playerData = header.GetData();
            
            health = InitializeComponent<PlayerHealth, PlayerRig>();
            
            InitializeComponent<PlayerMovement, PlayerRig>();
            InitializeComponent<PlayerBezierSpline, PlayerRig>();
            
            InitializeComponent<PlayerAttacker, PlayerRig>();
        }
        
        public PlayerData GetData() => playerData;

        #region Interface
        
        public override EUnitFaction Faction => EUnitFaction.Ally;
        public override bool IsPlayer => true;

        public override IUnitHealth Health => health;

        #endregion
    }
}