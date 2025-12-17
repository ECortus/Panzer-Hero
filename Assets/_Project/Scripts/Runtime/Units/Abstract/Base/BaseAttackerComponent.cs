using GameDevUtils.Runtime;
using PanzerHero.Runtime.Combat;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Abstract.Base
{
    public abstract class BaseAttackerComponent<T> : BaseRigComponent<T>
        where T : BaseRig
    {
        protected void SpawnBullet(BulletBehaviour prefab, Vector3 startPoint, Vector3 direction)
        {
            var bullet = ObjectInstantiator.InstantiatePrefabForComponent(prefab, startPoint, new Quaternion());
            bullet.LaunchInDirection(Rig, direction);
        }
    }
}