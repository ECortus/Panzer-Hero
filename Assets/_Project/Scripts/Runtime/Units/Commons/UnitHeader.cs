using System;
using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Data;
using UnityEngine;

namespace PanzerHero.Runtime.Units
{
    public class UnitHeader : BaseHeader<UnitRig>
    {
        [SerializeField] private UnitData playerData;
        
        public UnitData GetData() => playerData;
    }
}