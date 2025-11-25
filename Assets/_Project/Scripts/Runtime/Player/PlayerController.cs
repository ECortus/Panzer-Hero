using System;
using PanzerHero.Runtime.LevelDesign.Levels;
using PanzerHero.Runtime.Abstract;
using PanzerHero.Runtime.Systems;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PanzerHero.Runtime.Units.Player
{
    public class PlayerController : BaseController<PlayerRig>
    {
        protected override void OnStart()
        {
            base.OnStart();
            SetupEngineInputs();
        }

        void SetupEngineInputs()
        {
            var engine = GetComponent<PlayerEngine>();
            
            PlayerInputEvents inputEvents = PlayerInputEvents.GetInstance;
            inputEvents.OnMotorInput += (val) =>
            {
                if (val.y > 0)
                {
                    engine.setMotor(2);
                }
                else if (val.y < 0)
                {
                    engine.setMotor(-2);
                }
                else
                {
                    engine.setMotor(0);
                }
            };
        }
    }
}