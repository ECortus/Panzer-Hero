using System;
using UnityEngine;
using IsolarvHelperTools.Runtime;
using Zenject;

namespace PanzerHero.Runtime.LevelDesign.Levels
{
    public class LevelsCollector : UnitySingleton<LevelsCollector>
    {
        [Header("Debug")]
        [SerializeField] private LevelController[] levels;

        int currentLevelId;
        LevelController currentLevel;

        void SetCurrentLevel()
        {
            currentLevel = levels[currentLevelId];
        }
        
        public LevelController GetCurrentLevel()
        {
            return currentLevel;
        }

        void Awake()
        {
            Initialize();
        }
        
        void Initialize()
        {
            GetAllLevels();
        }

        void GetAllLevels()
        {
            levels = GetComponentsInChildren<LevelController>();
            for (int i = 0; i < levels.Length; i++)
            {
                var level = levels[i];
                level.Initialize();
            }

            SetCurrentLevel();
        }

        public void SetLevelId(int id)
        {
            currentLevelId = id;
            SetCurrentLevel();
        }

        public void UpLevelId()
        {
            currentLevelId++;
            if (currentLevelId >= levels.Length)
            {
                currentLevelId = 0;
            }

            SetCurrentLevel();
        }

        public void DownLevelId()
        {
            currentLevelId--;
            if (currentLevelId < 0)
            {
                currentLevelId = levels.Length - 1;
            }
            
            SetCurrentLevel();
        }

        public void RestartLevel()
        {
            for (int i = 0; i < levels.Length; i++)
            {
                var level = levels[i];
                level.Disable();
            }
            
            currentLevel.Enable();
            currentLevel.RestartLevel();
        }
    }
}