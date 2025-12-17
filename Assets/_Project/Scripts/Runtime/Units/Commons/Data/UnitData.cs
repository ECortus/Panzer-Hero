using PanzerHero.Runtime.Units.Abstract.Base;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Data
{
    [CreateAssetMenu(fileName = "New Unit Data", menuName = "Panzer Hero/Units/Data")]
    public class UnitData : ScriptableObject
    {
        public EUnitFaction Faction = EUnitFaction.Enemy;
        
        [Space(5)]
        public float maxHealth = 100f;
    }
}