using Cysharp.Threading.Tasks;
using IsolarvHelperTools.Runtime;
using UnityEngine;

namespace PanzerHero.Runtime.Systems
{
    public class PlayerInputSystem : AbstractCommonSystem
    {
        protected override async UniTask UpdateSystem()
        {
            await UniTask.Yield(this.GetCancellationTokenOnDestroy());
        }
    }
}