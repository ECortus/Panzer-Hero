using PanzerHero.Runtime.Currency;
using UnityEngine;
using Zenject;

namespace PanzerHero.Runtime.Installers
{
    public class CurrencyInstaller : MonoInstaller
    {
        [SerializeField] private CoinsManager coinsManager;
        [SerializeField] private DiamondsManager diamondsManager;
        
        public override void InstallBindings()
        {
            Container.Bind<CoinsManager>().FromInstance(coinsManager).AsSingle().NonLazy();
            Container.Bind<DiamondsManager>().FromInstance(diamondsManager).AsSingle().NonLazy();
        }
    }
}