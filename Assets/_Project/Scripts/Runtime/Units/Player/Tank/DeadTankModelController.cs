using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PanzerHero.Runtime.Units.Player.Tank
{
    public class DeadTankModelController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Rigidbody[] wheels;

        public void SetModelActive(bool active)
        {
            gameObject.SetActive(active);
        }
        
        public void AddForce(float force)
        {
            // var angular = Random.insideUnitSphere * Random.Range(90f, 360f);
            //
            // rb.AddForce(Vector3.up * force, ForceMode.VelocityChange);
            // rb.angularVelocity = angular;
            //
            // foreach (var wheel in wheels)
            // {
            //     var direction = (wheel.transform.position - transform.position).normalized;
            //     wheel.transform.parent = null;
            //     
            //     wheel.AddForce(direction * force * wheelsForceMod, ForceMode.Force);
            //     wheel.angularVelocity = angular;
            // }
        }
    }
}