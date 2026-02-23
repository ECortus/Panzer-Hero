using GameDevUtils.Runtime.UI;
using PanzerHero.Runtime.LevelDesign;
using SaveableExtension.Runtime.Saveable;
using UnityEngine;
using Zenject;

namespace PanzerHero.Runtime.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private DebugUI debugUI;
        [SerializeField] private SaveableSupervisor saveableSupervisor;

        [Space(5)] 
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private LevelDesignConfig levelDesignConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<DebugUI>().FromComponentInNewPrefab(debugUI).AsSingle().NonLazy();
            Container.Bind<SaveableSupervisor>().FromComponentInNewPrefab(saveableSupervisor).AsSingle().NonLazy();

            Container.Bind<GameConfig>().FromInstance(gameConfig).AsSingle().NonLazy();
            gameConfig.Init();
            
            Container.Bind<LevelDesignConfig>().FromInstance(levelDesignConfig).AsSingle().NonLazy();
            levelDesignConfig.Init();
        }
    }
}