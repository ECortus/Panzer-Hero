using System;
using GameDevUtils.Runtime;
using GameSaveKit.Runtime;
using GameSaveKit.Runtime.Saveable;
using PanzerHero.Runtime.LevelDesign.Levels;
using PanzerHero.Runtime.SavePrefs;
using PanzerHero.Runtime.Statistics;
using UnityEngine;

namespace PanzerHero.Runtime.LevelDesign
{
    public class GameStatement : UnitySingleton<GameStatement>
    {
        #region State

        enum EState
        {
            None,
            Launched,
            Started,
            Finished
        }

        [SerializeField] EState state = EState.None;
        
        void SetState(EState s)
        {
            var previousState = state;
            var currentState = s;
            
            this.state = s;
            
            DebugHelper.Log($"Game state changed FROM {previousState} TO {currentState}");
        }

        public bool IsOnLaunchedState() => state == EState.Launched;
        public bool IsOnStartedState() => state == EState.Started;
        public bool IsOnFinishedState() => state == EState.Finished;

        #endregion
        
        public event Action OnGameLaunched;
        public event Action OnGameStarted;
        public event Action OnGameFinished;

        private void Start()
        {
            LaunchGame();
            SaveablePrefs.LoadAll<PanzerHeroPrefs>();
        }

        void LaunchGame()
        {
            if (state == EState.Launched)
            {
                DebugHelper.Log("Game is already launched");
                return;
            }
            
            SetState(EState.Launched);
            OnGameLaunched?.Invoke();
        }

        public void RelaunchGame()
        {
            LaunchGame();
        }

        public void StartGame()
        {
            if (state == EState.Started)
            {
                DebugHelper.Log("Game is already started");
                return;
            }
            
            SetState(EState.Started);
            OnGameStarted?.Invoke();
        }

        public void FinishGame()
        {
            if (state == EState.Finished)
            {
                DebugHelper.Log("Game is already finished");
                return;
            }
            
            SetState(EState.Finished);
            OnGameFinished?.Invoke();
        }
    }
}