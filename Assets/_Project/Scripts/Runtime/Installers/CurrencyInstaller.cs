using PanzerHero.Runtime.Currency;
using UnityEngine;
using Zenject;

namespace PanzerHero.Runtime.Installers
{
    public class CurrencyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CoinsManager>().FromNewComponentOnNewGameObject().UnderTransform(transform).AsSingle().NonLazy();
            Container.Bind<DiamondsManager>().FromNewComponentOnNewGameObject().UnderTransform(transform).AsSingle().NonLazy();
        }
    }
}