using System;
using UnityEngine;
using IsolarvHelperTools.Runtime;
using Zenject;

namespace PanzerHero.Runtime.LevelDesign.Levels
{
    public class LevelManager : UnitySingleton<LevelManager>
    {
        [Header("Debug")]
        [SerializeField] private LevelController[] levels;

        int currentLevelId;
        LevelController currentLevel => levels[currentLevelId];

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
        }

        public void SetLevelId(int id)
        {
            currentLevelId = id;
        }

        public void UpLevelId()
        {
            currentLevelId++;
            if (currentLevelId >= levels.Length)
            {
                currentLevelId = 0;
            }
        }

        public void DownLevelId()
        {
            currentLevelId--;
            if (currentLevelId < 0)
            {
                currentLevelId = levels.Length - 1;
            }
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
        
        public IRoadSplineData GetRoadSplineData()
        {
            return currentLevel.GetRoadSplineData();
        }
    }
}