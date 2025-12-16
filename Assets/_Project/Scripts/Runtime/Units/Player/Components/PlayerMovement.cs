using System;
using PanzerHero.Runtime.Abstract;
using PanzerHero.Runtime.LevelDesign;
using PanzerHero.Runtime.Systems;
using PanzerHero.Runtime.Units.Player;
using PanzerHero.Runtime.Units.Player.Data;
using UnityEngine;

namespace PanzerHero.Runtime.Player
{
    public class PlayerMovement : BaseRigComponent<PlayerRig>
    {
        PlayerHeader header;
        PlayerBezierSpline spline;
        PlayerEngine engine;
        PlayerInputEvents inputEvents;
        
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
            
            header = GetComponent<PlayerHeader>();
            spline = GetComponent<PlayerBezierSpline>();
            engine = GetComponent<PlayerEngine>();
            
            playerData = header.GetData();
            
            SetupEngineInputs();
            SetupEvents();
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

        void SetupEvents()
        {
            var statement = GameStatement.GetInstance;
            if (!statement)
                return;
            
            statement.OnGameLaunched += SetBodyKinematic;
            
            statement.OnGameStarted += SetBodyPhysical;
            
            statement.OnGameFinished += SetBodyKinematic;
            statement.OnGameFinished += StopBody;
        }

        void RemoveEvents()
        {
            var statement = GameStatement.GetInstance;
            if (!statement)
                return;
            
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

        void Update()
        {
            if (!spline.CanCalculate())
            {
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
        }

        void UpdateRotation(float deltaTime = 100f)
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
            
            var targetRotation = Quaternion.LookRotation(lookDirection);
            var rotation = Quaternion.Lerp(transform.rotation, targetRotation, playerData.rotationSpeed * deltaTime);
            
            SetRotation(rotation);
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
            StopBody();
            
            Vector3 targetPoint = point;
            if (Physics.Raycast(point, Vector3.down, out RaycastHit hit, 100, 1 << LayerMask.NameToLayer("Ground")))
            {
                targetPoint = hit.point;
            }

            targetPoint.y += sphere.radius;
            
            SetPosition(targetPoint);
            UpdateRotation();
        }
        
        protected override void OnDestroy()
        {
            RemoveEngineInputs();
            RemoveEvents();
        }
    }
}