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
        
        protected class Timer
        {
            float lastTime;
            readonly float Delay;
            
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

            public void Reset()
            {
                lastTime = Time.time;
            }
        }
    }
}