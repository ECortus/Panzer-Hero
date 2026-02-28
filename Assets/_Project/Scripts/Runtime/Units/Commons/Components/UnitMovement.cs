using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Data;
using UnityEngine;
using UnityEngine.AI;

namespace PanzerHero.Runtime.Units.Components
{
    public class UnitMovement : BaseRigComponent<UnitRig>
    {
        NavMeshAgent agent;

        UnitData data;
        
        public float GetSpeed()
        {
            return agent.velocity.magnitude;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            agent = GetComponent<NavMeshAgent>();
            
            data = Rig.GetData();

            agent.speed = data.movementSpeed;
            agent.acceleration = data.accelerationSpeed;
            agent.angularSpeed = data.angularSpeed;
            
            agent.autoBraking = false;
        }

        public void SetDestination(Vector3 targetPoint)
        {
            agent.isStopped = false;
            agent.SetDestination(targetPoint);
        }
        
        public void Stop()
        {
            agent.isStopped = true;
        }

        public Vector3 GetCurrentPosition()
        {
            var position = transform.position;
            if (NavMesh.SamplePosition(position, out NavMeshHit hit, 15.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
            
            return position;
        }
    }
}