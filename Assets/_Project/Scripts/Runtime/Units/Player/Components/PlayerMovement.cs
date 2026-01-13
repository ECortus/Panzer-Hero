using System;
using PanzerHero.Runtime.LevelDesign;
using PanzerHero.Runtime.LevelDesign.Levels;
using PanzerHero.Runtime.Systems;
using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Player.Data;
using PanzerHero.Runtime.Units.Simultaneous;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player.Components
{
    public class PlayerMovement : BaseRigComponent<PlayerRig>
    {
        GameStatement statement;
        PlayerInputEvents inputEvents;
        LevelManager levelManager;
        
        PlayerBezierSpline spline;
        VehicleEngine engine;
        
        PlayerData playerData;
        
        Rigidbody rb;
        SphereCollider sphere;

        Vector2 motorInput;

        Vector3 previousPosition;
        Vector3 currentPosition;

        int speedSign;
        float calculatedSpeed;
        
        public override void Initialize()
        {
            base.Initialize();
            
            rb = GetComponent<Rigidbody>();
            sphere = GetComponent<SphereCollider>();
            
            spline = GetComponent<PlayerBezierSpline>();
            
            engine = GetComponent<VehicleEngine>();
            
            playerData = Rig.GetData();
            
            var engineData = playerData.engineData;
            engineData.SetupEngineData(engine);
            
            SetupEngineInputs();
            SetupEvents();
        }

        void SetupEngineInputs()
        {
            inputEvents = PlayerInputEvents.GetInstance;
            inputEvents.OnMotorInput += SetMotor;

            levelManager = LevelManager.GetInstance;
            levelManager.OnLevelChanged += ResetMotor;
        }
        
        void RemoveEngineInputs()
        {
            inputEvents.OnMotorInput -= SetMotor;
            levelManager.OnLevelChanged -= ResetMotor;
        }

        void SetupEvents()
        {
            statement = GameStatement.GetInstance;
            
            statement.OnGameLaunched += SetBodyKinematic;
            
            statement.OnGameStarted += SetBodyPhysical;
            
            statement.OnGameFinished += SetBodyKinematic;
            statement.OnGameFinished += StopBody;
        }

        void RemoveEvents()
        {
            statement.OnGameLaunched -= SetBodyKinematic;
            
            statement.OnGameStarted -= SetBodyPhysical;
            
            statement.OnGameFinished -= SetBodyKinematic;
            statement.OnGameFinished -= StopBody;
        }

        void SetMotor(Vector2 val)
        {
            motorInput = val;
        }
        
        void SetBodyPhysical()
        {
            rb.isKinematic = true;
        }
        
        void SetBodyKinematic()
        {
            rb.isKinematic = false;
        }

        void ResetMotor()
        {
            SetMotor(Vector2.zero);
        }

        void Update()
        {
            if (!spline.CanCalculate())
            {
                return;
            }

            if (statement.IsOnFinishedState())
            {
                StopBody();
                return;
            }
            
            UpdateMotor();
            
            if (motorInput.y > 0 && spline.IsPathFinished())
            {
                StopBody();
                return;
            }
            
            if (motorInput.y < 0 && spline.IsPathNotStarted())
            {
                StopBody();
                return;
            }
            
            UpdateSpeed();
            if (calculatedSpeed == 0)
            {
                return;
            }

            if (statement.IsOnLaunchedState())
            {
                UpdateRotationRoughly();
            }
            else
            {
                UpdateRotationSmoothly(Time.deltaTime);
            }
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
            calculatedSpeed = (currentPosition - previousPosition).sqrMagnitude;
            
            var speedDirection = (currentPosition - previousPosition).normalized;
            var directionOfSegment = spline.GetDirectionOfSegment();

            var signValue = Vector3.Dot(directionOfSegment, speedDirection);
            speedSign = Math.Sign(signValue);
            
            previousPosition = currentPosition;
        }

        void ResetSpeed()
        {
            currentPosition = spline.GetStartPoint();
            previousPosition = currentPosition;
            
            calculatedSpeed = 0;
            speedSign = 0;
        }

        void UpdateRotationSmoothly(float deltaTime)
        {
            var targetRotation = GetLookRotation();
            var rotation = Quaternion.Lerp(rb.rotation, targetRotation, playerData.rotationSpeed * deltaTime);
            
            SetRotation(rotation);
        }

        void UpdateRotationRoughly()
        {
            var targetRotation = GetLookRotation();
            SetRotation(targetRotation);
        }

        Quaternion GetLookRotation()
        {
            Vector3 lookDirection;
            if (speedSign >= 0)
            {
                lookDirection = spline.GetDirectionToNext();
            }
            else
            {
                lookDirection = -spline.GetDirectionToPrevious();
            }

            return Quaternion.LookRotation(lookDirection);
        }

        void SetRotation(Quaternion rotation)
        {
            var angles = rotation.eulerAngles;
            angles.x = transform.eulerAngles.x; angles.z = transform.eulerAngles.z;
            
            var targetRotation = Quaternion.Euler(angles);
            rotation = targetRotation;

            if (rb.isKinematic)
            {
                transform.rotation = rotation;
            }
            else
            {
                rb.MoveRotation(rotation);
            }
        }

        void SetPosition(Vector3 position)
        {
            if (rb.isKinematic)
            {
                transform.position = position;
            }
            else
            {
                rb.MovePosition(position);
            }
        }

        void StopBody()
        {
            engine.setMotor(0);
            
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            
            ResetSpeed();
        }
        
        public void TeleportToPoint(Vector3 point)
        {
            SetBodyKinematic();
            
            StopBody();
            
            Vector3 targetPoint = point;
            if (Physics.Raycast(point, Vector3.down, out RaycastHit hit, 100, 1 << LayerMask.NameToLayer("Ground")))
            {
                targetPoint = hit.point;
            }

            targetPoint.y += sphere.radius;
            
            SetPosition(targetPoint);
            UpdateRotationRoughly();
            
            SetBodyPhysical();
        }
        
        protected override void OnDestroy()
        {
            RemoveEngineInputs();
            RemoveEvents();
        }
    }
}