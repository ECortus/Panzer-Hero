using System;
using PanzerHero.Runtime.Units.Simultaneous;
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
    }
}