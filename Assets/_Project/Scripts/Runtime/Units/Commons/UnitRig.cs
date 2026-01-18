using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Components;
using PanzerHero.Runtime.Units.Data;

namespace PanzerHero.Runtime.Units
{
    public class UnitRig : BaseRig
    {
        UnitHeader header;
        
        UnitData unitData;
        UnitHealth health;
        
        protected override void InitializeComponents()
        {
            header = GetComponent<UnitHeader>();
            unitData = header.GetData();
            
            health = InitializeComponent<UnitHealth, UnitRig>();
            
            InitializeComponent<UnitMovement, UnitRig>();
            InitializeComponent<UnitAttacker, UnitRig>();

            InitializeComponent<UnitAI, UnitRig>();
        }
        
        public UnitData GetData() => unitData;

        #region Interface
        
        public override EUnitFaction Faction => unitData.Faction;
        public override bool IsPlayer => false;

        public override IUnitHealth Health => health;

        #endregion
    }
}