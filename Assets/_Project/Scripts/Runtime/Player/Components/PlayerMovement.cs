using PanzerHero.Runtime.Systems;
using PanzerHero.Runtime.Units.Components;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player
{
    public class PlayerMovement : RigComponent
    {
        PlayerBezierSpline spline;
        private Rigidbody rb;

        private Vector3 previousPosition;
        private Vector3 currentPosition;

        private float calculatedSpeed;
        
        public override void Initialize()
        {
            base.Initialize();
            spline = GetComponent<PlayerBezierSpline>();
            rb = GetComponent<Rigidbody>();
            
            SetupEngineInputs();
            ResetSpeed();
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

        void Update()
        {
            UpdateSpeed();
            if (calculatedSpeed <= 0)
            {
                return;
            }
            
            UpdateBezierSpline();
        }

        void UpdateBezierSpline()
        {
            spline.MovePointAlongSpline(calculatedSpeed);
            
            var targetRotation = spline.RotateAlongSpline();
            rb.MoveRotation(targetRotation);
        }

        void UpdateSpeed()
        {
            currentPosition = transform.position;
            calculatedSpeed = Vector3.Distance(currentPosition, previousPosition);
            
            previousPosition = currentPosition;
        }

        void ResetSpeed()
        {
            currentPosition = transform.position;
            previousPosition = transform.position;
            calculatedSpeed = 0;
        }
    }
}