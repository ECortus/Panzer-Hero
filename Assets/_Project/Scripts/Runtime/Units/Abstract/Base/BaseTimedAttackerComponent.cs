using System;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Abstract.Base
{
    public abstract class BaseTimedAttackerComponent<T> : BaseAttackerComponent<T>
        where T : BaseRig
    {
        protected Timer CreateNewTimer(float delay)
        {
            return new Timer(delay);
        }
        
        public interface ITimerInfo
        {
            public float DelayTime { get; }
            public float LeftTime { get; }
        }
        
        public class Timer : ITimerInfo
        {
            float lastTime;
            float Delay;
            
            public Timer(float delay)
            {
                Delay = delay;
                Reset();
            }
            
            public bool CanDoAction()
            {
                var currentTime = Time.time;
                if (currentTime - lastTime > Delay)
                {
                    return true;
                }

                return false;
            }

            public void ChangeDelay(float delay)
            {
                Delay = delay;
            }

            public void Reset()
            {
                lastTime = Time.time;
            }

            #region Interface

            public float DelayTime => Delay;
            public float LeftTime
            {
                get
                {
                    var currentTime = Time.time;
                    if (currentTime - lastTime > Delay)
                    {
                        return 0;
                    }

                    return MathF.Round(currentTime - lastTime, 2);
                }
            }

            #endregion
        }
    }
}