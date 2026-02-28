using PanzerHero.Runtime.Ragdoll;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Components
{
    [RequireComponent(typeof(RagdollController))]
    public class UnitRagdollController : MonoBehaviour
    {
        Animator animator;
        RagdollController ragdollController;
        
        UnitHealth unitHealth;
        
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            ragdollController = GetComponent<RagdollController>();
            
            unitHealth = GetComponentInParent<UnitHealth>();
            unitHealth.OnDied += SetAsRagdoll;
            
            SetAsRegular();
        }

        void SetAsRegular()
        {
            animator.enabled = true;
            ragdollController.SetAsRegular();
        }
        
        void SetAsRagdoll()
        {
            animator.enabled = false;
            ragdollController.SetAsRagdoll();
        }
    }
}