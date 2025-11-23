using Cysharp.Threading.Tasks;
using IsolarvHelperTools.Runtime;
using UnityEngine;

namespace PanzerHero.Runtime.Systems
{
    public class PlayerInputSystem : AbstractCommonSystem
    {
        protected override UniTask UpdateSystem(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartGas();
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                StopGas();
            }
            
            return UniTask.CompletedTask;
        }

        void StartGas()
        {
            
        }

        void StopGas()
        {
            
        }
    }
}