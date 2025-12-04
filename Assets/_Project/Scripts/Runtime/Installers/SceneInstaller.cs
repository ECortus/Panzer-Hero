using PanzerHero.Runtime.LevelDesign;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace PanzerHero.Runtime.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameStatement>().FromNewComponentOnNewGameObject().UnderTransform(transform).AsSingle().NonLazy();
        }
    }
}