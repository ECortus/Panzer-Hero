using System;
using PanzerHero.Runtime.Systems;
using PanzerHero.Runtime.Units.Components;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player
{
    public class PlayerMovement : RigComponent
    {
        PlayerBezierSpline spline;
        PlayerEngine engine;
        PlayerInputEvents inputEvents;
        
        Rigidbody rb;

        Vector2 motorInput;

        Vector3 previousPosition;
        Vector3 currentPosition;

        float calculatedSpeed;
        
        public override void Initialize()
        {
            base.Initialize();
            
            rb = GetComponent<Rigidbody>();
            
            spline = GetComponent<PlayerBezierSpline>();
            engine = GetComponent<PlayerEngine>();
            
            SetupEngineInputs();
            SpawnAtStartPoint();
            
            ResetSpeed();
        }

        void SetupEngineInputs()
        {
            inputEvents = PlayerInputEvents.GetInstance;
            inputEvents.OnMotorInput += SetMotor;
        }
        
        void RemoveEngineInputs()
        {
            inputEvents.OnMotorInput -= SetMotor;
        }

        void SetMotor(Vector2 val)
        {
            motorInput = val;
        }

        void SpawnAtStartPoint()
        {
            var startPoint = spline.GetStartPoint();

            startPoint.y = 1.25f;
            transform.position = startPoint;
            rb.position = startPoint;
            
            UpdateRotation(25f);
        }

        void Update()
        {
            if (spline.PathIsFinished)
            {
                StopBody();
                return;
            }
            
            UpdateMotor();
            UpdateSpeed();
            
            if (calculatedSpeed == 0)
            {
                return;
            }
            
            UpdateRotation(Time.deltaTime);
        }

        void UpdateMotor()
        {
            if (motorInput.y == 0)
            {
                engine.setMotor(0);
                return;
            }
            
            var val = Mathf.Sign(motorInput.y);
            engine.setMotor(val);
        }
        
        void UpdateSpeed()
        {
            currentPosition = transform.position;
            calculatedSpeed = Vector3.Distance(currentPosition, previousPosition);
            previousPosition = currentPosition;
        }

        void ResetSpeed()
        {
            currentPosition = spline.GetStartPoint();
            previousPosition = currentPosition;
            calculatedSpeed = 0;
        }

        void UpdateRotation(float deltaTime)
        {
            var lookDirection = spline.GetDirection();
            var targetRotation = Quaternion.LookRotation(lookDirection);
            var rotation = Quaternion.Slerp(transform.rotation, targetRotation, deltaTime * 25f);
            
            SetRotation(rotation);
        }

        void SetRotation(Quaternion rotation)
        {
            var angles = rotation.eulerAngles;
            angles.x = transform.eulerAngles.x; angles.z = transform.eulerAngles.z;
            
            var targetRotation = Quaternion.Euler(angles);
            rotation = targetRotation;
            
            rb.MoveRotation(rotation);
        }

        void StopBody()
        {
            engine.setMotor(0);
            
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        
        private void OnDestroy()
        {
            RemoveEngineInputs();
        }
    }
}