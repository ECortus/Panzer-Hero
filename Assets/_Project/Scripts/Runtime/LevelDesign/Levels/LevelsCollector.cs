using System;
using UnityEngine;
using GameDevUtils.Runtime;
using Zenject;

namespace PanzerHero.Runtime.LevelDesign.Levels
{
    public class LevelsCollector : UnitySingleton<LevelsCollector>
    {
        [Header("Debug")]
        [SerializeField] private LevelController[] levels;

        int currentLevelId;
        LevelController currentLevel;

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
        
        void SetCurrentLevel()
        {
            currentLevel = levels[currentLevelId];
            RestartLevel();
        }
        
        public ILevelData GetLevelData()
        {
            return currentLevel;
        }
        
        public int GetLevelId()
        {
            return currentLevelId;
        }

        public void SetLevelId(int id)
        {
            currentLevelId = id;
            SetCurrentLevel();
        }

        public void SetNextLevel()
        {
            SetNextID();
            SetCurrentLevel();
        }

        public void SetPreviousLevel()
        {
            SetPreviousID();
            SetCurrentLevel();
        }
        
        public void SetNextID()
        {
            currentLevelId++;
            if (currentLevelId >= levels.Length)
            {
                currentLevelId = 0;
            }
        }

        public void SetPreviousID()
        {
            currentLevelId--;
            if (currentLevelId < 0)
            {
                currentLevelId = levels.Length - 1;
            }
        }

        public void RestartLevel()
        {
            DisableAllLevels();
            
            currentLevel.Enable();
            currentLevel.RestartLevel();
        }
        
        void DisableAllLevels()
        {
            for (int i = 0; i < levels.Length; i++)
            {
                var level = levels[i];
                level.Disable();
            }
        }
    }
}