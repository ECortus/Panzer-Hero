using PanzerHero.Runtime.LevelDesign;
using UnityEngine;
using Zenject;

namespace PanzerHero.Runtime.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameStatement>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}