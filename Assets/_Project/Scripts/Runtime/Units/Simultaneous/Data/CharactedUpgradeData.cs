using GameDevUtils.Runtime.Simultaneous;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Simultaneous
{
    [CreateAssetMenu(fileName = "UpgradedCharactedData", menuName = "Panzer Hero/Units/Simultaneous/UpgradedCharactedData")]
    public class UpgradedCharactedData : ScriptableObject
    {
        [SerializeField] private ActorInformation info;
        public IActorInformation Info => info;

        [Space(10)]
        public int maxProgressLevel = 5;

        public float[] progressValues = new float[5];
        public float[] progressCost = new float[5];
    }
}