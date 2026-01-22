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

        public override void Initialize()
        {
            base.Initialize();
            
            agent = GetComponent<NavMeshAgent>();
            
            data = Rig.GetData();

            agent.speed = data.movementSpeed;
            agent.acceleration = data.accelerationSpeed;
            agent.angularSpeed = data.angularSpeed;
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
    }
}