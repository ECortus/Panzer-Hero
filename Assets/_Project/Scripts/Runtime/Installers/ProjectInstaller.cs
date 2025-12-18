using GameDevUtils.Runtime.UI;
using GameSaveKit.Runtime.Saveable;
using UnityEngine;
using Zenject;

namespace PanzerHero.Runtime.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private IsolarvDebugUI debugUI;
        [SerializeField] private SaveableSupervisor saveableSupervisor;
        
        public override void InstallBindings()
        {
            Container.Bind<IsolarvDebugUI>().FromComponentInNewPrefab(debugUI).AsSingle().NonLazy();
            Container.Bind<SaveableSupervisor>().FromComponentInNewPrefab(saveableSupervisor).AsSingle().NonLazy();
        }
    }
}