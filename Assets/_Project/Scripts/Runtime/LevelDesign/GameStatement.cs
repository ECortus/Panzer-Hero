using GameDevUtils.Runtime;
using UnityEngine;

namespace PanzerHero.Runtime.LevelDesign
{
    public class GameStatement : UnitySingleton<GameStatement>
    {
        public enum EState
        {
            Started,
            InProgress,
            Finished
        }
        
        [SerializeField] EState state;
        
        public EState GetState()
        {
            return state;
        }
        
        public void SetState(EState s)
        {
            this.state = s;
        }

        public void StartGame()
        {
            SetState(EState.Started);
            Debug.Log("Game Started");
        }

        public void FinishGame()
        {
            SetState(EState.Finished);
            Debug.Log("Game Finished");
        }
    }
}