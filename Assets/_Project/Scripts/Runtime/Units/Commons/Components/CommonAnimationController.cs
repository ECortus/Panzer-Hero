using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Components
{
    public class CommonAnimationController : MonoBehaviour
    {
        readonly int SpeedID = Animator.StringToHash("Speed");
        readonly int IsShootingID = Animator.StringToHash("IsShooting");

        const float PrepareShootingTime = 0.25f;

        Animator animator;

        UnitAI unitAI;
        UnitAttacker attacker;
        
        CancellationTokenSource preparingTokenSource;

        void Start()
        {
            animator = GetComponent<Animator>();
            
            unitAI = GetComponentInParent<UnitAI>();
            attacker = GetComponentInParent<UnitAttacker>();
            
            unitAI.OnStateChange += UpdateAnimation;
        }
        
        void OnDestroy()
        {
            unitAI.OnStateChange -= UpdateAnimation;
        }
        
        void UpdateAnimation(UnitAI.EUnitState state)
        {
            switch (state)
            {
                case UnitAI.EUnitState.Idle:
                    SetSpeed(0);
                    SetIsShooting(false);
                    break;
                case UnitAI.EUnitState.Patrol:
                    SetSpeed(1);
                    SetIsShooting(false);
                    break;
                case UnitAI.EUnitState.Chase:
                    SetSpeed(1);
                    SetIsShooting(false);
                    break;
                case UnitAI.EUnitState.Attack:
                    SetSpeed(0);
                    SetIsShooting(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        async void PrepareShooting()
        {
            preparingTokenSource = new CancellationTokenSource();
            
            attacker.SetBlockToFire(true);
            await UniTask.Delay((int)(PrepareShootingTime * 1000), cancellationToken: preparingTokenSource.Token);
            attacker.SetBlockToFire(false);
            
            preparingTokenSource = null;
        }
        
        void CancelPrepareShooting()
        {
            preparingTokenSource?.Cancel();
            preparingTokenSource = null;
        }

        void SetSpeed(float speed)
        {
            animator.SetFloat(SpeedID, speed);
        }
        
        void SetIsShooting(bool isShooting)
        {
            bool previous = animator.GetBool(IsShootingID);
            
            if (previous != isShooting && isShooting)
            {
                PrepareShooting();
            }
            
            if (previous != isShooting && !isShooting)
            {
                CancelPrepareShooting();
            }
            
            animator.SetBool(IsShootingID, isShooting);
        }
    }
}