using PanzerHero.Runtime.Ragdoll;
using PanzerHero.Runtime.Statistics;
using UnityEngine;

namespace PanzerHero.Runtime.Destrictable
{
    [RequireComponent(typeof(RagdollController))]
    public class HouseDestrictable : DestrictableObject
    {
        RagdollController ragdollController;
        LevelStatistics statistics;

        protected override void Awake()
        {
            statistics = LevelStatistics.GetInstance;
            
            ragdollController = GetComponent<RagdollController>();
            base.Awake();
        }

        protected override void RenewProcess()
        {
            ragdollController.SetAsRegular();
        }

        protected override void DestroyProcess()
        {
            statistics.HousesDestroyed.AddSingular();
            ragdollController.SetAsRagdoll();
        }
    }
}