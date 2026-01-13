using System;
using PanzerHero.Runtime.Units.Player.Components;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Simultaneous
{
    [Serializable]
    public class VehicleEngineData
    {
        [Header("Physics")]
        [Tooltip("Radius of the sphere collider.")]
        public float colliderRadius = 2f;
        [Tooltip("Mass of the rigid body.")]
        public float bodyMass = 1f;
        
        [Space(5)]
        [Tooltip("Always Down = Gravity always points straight down.\nTowards Ground = Gravity points to the surface when the car is grounded, otherwise it points straight down.")]
        public VehicleEngine.GRAVITY_MODE gravityMode = VehicleEngine.GRAVITY_MODE.TowardsGround;
        [Tooltip("Gravity speed.")]
        public float gravityVelocity = 80;
        [Tooltip("Maximum gravity.")]
        public float maxGravity = 50;
        
        [Space(5)]
        [Tooltip("Maximum angle of climbable slopes.")]
        public float maxSlopeAngle = 50f;
        [Tooltip("Amount of friction applied when colliding with a wall.")]
        public float sideFriction = 1f;
        [Tooltip("The layers that will be used for ground checks.")]
        public LayerMask collidableLayers = ~0;
        [Tooltip("Scales the mass, velocity, and gravity according to the GameObject's scale.")]
        public bool adjustToScale = false;

        [Header("Engine")]
        [Tooltip("How much acceleration to apply relative to the speed of the car.")]
        public AnimationCurve accelerationCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1), new Keyframe(1, 0) });
        
        [Space(5)]
        [Tooltip("Maximum acceleration when going forward.")]
        public float maxAccelerationForward = 100;
        [Tooltip("Maximum speed when going forward.")]
        public float maxSpeedForward = 40;
        [Tooltip("Maximum acceleration when going in reverse.")]
        public float maxAccelerationReverse = 50;
        [Tooltip("Maximum speed when going in reverse.")]
        public float maxSpeedReverse = 20;
        
        [Space(5)]
        [Tooltip("How fast the car will brake when the motor goes in the opposite direction.")]
        public float brakeStrength = 200;
        [Tooltip("How much friction to apply when on a slope. The higher this value, the slower you'll climb up slopes and the faster you'll go down. Setting this to zero adds no additional friction.")]
        public float slopeFriction = 1f;

        [Header("Steering")]
        [Tooltip("Sharpness of the steering.")]
        public float maxSteering = 200;
        [Tooltip("Multiplier applied to steering when in the air. Setting this to zero makes the car unsteerable in the air.")]
        public float steeringMultiplierInAir = 0.25f;
        [Tooltip("How much steering to apply relative to the speed of the car.")]
        public AnimationCurve steeringBySpeed = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(0.25f, 1), new Keyframe(1, 1) });
        [Tooltip("How fast the car stops when releasing the gas.")]
        public float forwardFriction = 40;
        [Tooltip("How much grip the car should have on the road when turning.")]
        public float lateralFriction = 80;

        public void SetupEngineData(VehicleEngine engine)
        {
            engine.colliderRadius = colliderRadius;
            engine.bodyMass = bodyMass;
            
            engine.gravityMode = gravityMode;
            engine.gravityVelocity = gravityVelocity;
            engine.maxGravity = maxGravity;
            
            engine.maxSlopeAngle = maxSlopeAngle;
            engine.sideFriction = sideFriction;
            engine.collidableLayers = collidableLayers;
            engine.adjustToScale = adjustToScale;
            
            engine.accelerationCurve = accelerationCurve;
            
            engine.maxAccelerationForward = maxAccelerationForward;
            engine.maxSpeedForward = maxSpeedForward;
            engine.maxAccelerationReverse = maxAccelerationReverse;
            engine.maxSpeedReverse = maxSpeedReverse;
            engine.brakeStrength = brakeStrength;
            engine.slopeFriction = slopeFriction;
            
            engine.maxSteering = maxSteering;
            engine.steeringMultiplierInAir = steeringMultiplierInAir;
            engine.steeringBySpeed = steeringBySpeed;
            engine.forwardFriction = forwardFriction;
            engine.lateralFriction = lateralFriction;
        }
    }
}