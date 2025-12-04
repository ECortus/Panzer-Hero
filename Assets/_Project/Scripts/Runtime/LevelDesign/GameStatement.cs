using System;
using GameDevUtils.Runtime;
using PanzerHero.Runtime.LevelDesign.Levels;
using UnityEngine;

namespace PanzerHero.Runtime.LevelDesign
{
    public class GameStatement : UnitySingleton<GameStatement>
    {
        #region State

        public enum EState
        {
            NonStarted,
            InProgress,
            Finished
        }

        [SerializeField] EState state = EState.NonStarted;
        
        public EState GetState()
        {
            return state;
        }
        
        void SetState(EState s)
        {
            this.state = s;
        }

        #endregion
        
        public event Action OnGameLaunched;
        public event Action OnGameStarted;
        public event Action OnGameFinished;

        private void Start()
        {
            RestartGame();
        }

        public void LaunchGame()
        {
            if (state == EState.NonStarted)
            {
                DebugHelper.LogError("Game is already launched started");
                return;
            }
            
            SetState(EState.NonStarted);
            OnGameLaunched?.Invoke();
        }

        public void StartGame()
        {
            if (state != EState.NonStarted)
            {
                DebugHelper.LogError("Game is not LAUNCHED, can't START GAME");
                return;
            }
            
            if (state == EState.InProgress)
            {
                DebugHelper.LogError("Game is already in progress");
                return;
            }
            
            SetState(EState.InProgress);
            OnGameStarted?.Invoke();
        }

        public void FinishGame()
        {
            if (state != EState.InProgress)
            {
                DebugHelper.LogError("Game is not IN PROGRESS, can't FINISH GAME");
                return;
            }
            
            if (state == EState.Finished)
            {
                DebugHelper.LogError("Game is already finished");
                return;
            }
            
            SetState(EState.Finished);
            OnGameFinished?.Invoke();
        }
        
        public void RestartGame()
        {
            var collector = LevelsCollector.GetInstance;
            collector.RestartLevel();
            
            LaunchGame();
            StartGame();
        }
        
        public void GoNextGame()
        {
            var collector = LevelsCollector.GetInstance;
            collector.SetNextLevel();
        }
    }
}