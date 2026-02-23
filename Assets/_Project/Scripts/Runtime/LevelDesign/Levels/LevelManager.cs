using System;
using UnityEngine;
using GameDevUtils.Runtime;
using PanzerHero.Runtime.SavePrefs;
using SaveableExtension.Runtime.Saveable;

namespace PanzerHero.Runtime.LevelDesign.Levels
{
    public class LevelManager : UnitySingleton<LevelManager>, ISaveableBehaviour<PanzerHeroPrefs>
    {
        [Header("Debug")]
        [SerializeField] private Level[] levels;
        
        int currentLevelId = 0;
        Level currentLevel;
        
        public event Action OnLevelChanged;

        protected override void OnAwake()
        {
            Initialize();
            
            var statement = GameStatement.GetInstance;
            statement.OnGameLaunched += SetCurrentLevel;
            
            SaveableSupervisor.AddBehaviour(this);
        }

        private void OnDestroy()
        {
            SaveableSupervisor.RemoveBehaviour(this);
        }
        
        void Initialize()
        {
            GetAllLevels();
        }

        void GetAllLevels()
        {
            levels = GetComponentsInChildren<Level>();
            for (int i = 0; i < levels.Length; i++)
            {
                var level = levels[i];
                level.Initialize();
            }
        }
        
        public int GetLevel()
        {
            return currentLevelId;
        }
        
        void SetCurrentLevel()
        {
            SetLevel(currentLevelId);
        }

        public void SetLevel(int id)
        {
            for (int i = 0; i < levels.Length; i++)
            {
                var level = levels[i];
                if (i == id)
                {
                    currentLevel = level;
                    level.Enable();
                }
                else
                {
                    level.Disable();
                }
            }

            ResetLevel();
            
            currentLevelId = id;
            OnLevelChanged?.Invoke();
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
        
        public ILevelData GetLevelData()
        {
            return currentLevel;
        }

        void ResetLevel()
        {
            currentLevel.Reseting();
        }

        public void Serialize(ref PanzerHeroPrefs record)
        {
            record.LevelID = currentLevelId;
        }
        
        public void Deserialize(PanzerHeroPrefs record)
        {
            SetLevel(record.LevelID);
        }
    }
}