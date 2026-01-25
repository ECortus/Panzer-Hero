using System;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Abstract.Base
{
    public interface ITimerInfo
    {
        public float DelayProgress { get; }
    }
    
    public abstract class BaseTimedAttackerComponent<T> : BaseAttackerComponent<T>
        where T : BaseRig
    {
        protected Timer CreateNewTimer(float delay)
        {
            return new Timer(delay);
        }
        
        public class Timer : ITimerInfo
        {
            float lastTime;
            readonly float Delay;
            
            public Timer(float delay)
            {
                Delay = delay;
                lastTime = Time.time - delay;
            }
            
            float delayProgress
            {
                get
                {
                    var currentTime = Time.time;
                    var difference = currentTime - lastTime;
                    
                    return Mathf.Clamp01(difference / Delay);
                }
            }
            
            public bool CanDoAction()
            {
                return delayProgress >= 1;
            }

            public void Reset()
            {
                lastTime = Time.time;
            }

            #region Interface

            public float DelayProgress => delayProgress;

            #endregion
        }
    }
}