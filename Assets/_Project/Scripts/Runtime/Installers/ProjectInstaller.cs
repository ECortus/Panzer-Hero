using GameDevUtils.Runtime.UI;
using GameSaveKit.Runtime.Saveable;
using PanzerHero.Runtime.LevelDesign;
using PanzerHero.Runtime.LevelDesign.Rewards;
using UnityEngine;
using Zenject;

namespace PanzerHero.Runtime.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private IsolarvDebugUI debugUI;
        [SerializeField] private SaveableSupervisor saveableSupervisor;

        [Space(5)] 
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private LevelDesignConfig levelDesignConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<IsolarvDebugUI>().FromComponentInNewPrefab(debugUI).AsSingle().NonLazy();
            Container.Bind<SaveableSupervisor>().FromComponentInNewPrefab(saveableSupervisor).AsSingle().NonLazy();
            
            gameConfig.Init();
            levelDesignConfig.Init();
        }
    }
}