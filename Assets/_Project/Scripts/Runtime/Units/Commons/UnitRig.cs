using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Components;
using PanzerHero.Runtime.Units.Data;
using UnityEngine;

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

            InitializeComponent<UnitTargetCalculator, UnitRig>();
            
            InitializeComponent<UnitAI, UnitRig>();
        }
        
        public UnitData GetData() => unitData;

        #region Interface
        
        public override Vector3 Position => transform.position;
        
        public override EUnitFaction Faction => unitData.Faction;

        public override IUnitHealth Health => health;

        #endregion
    }
}