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
            None,
            Launched,
            Started,
            Finished
        }

        [SerializeField] EState state = EState.None;
        
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
            LaunchGame();
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