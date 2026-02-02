using System;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player.Tank
{
    public class DeadTankModelController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Rigidbody[] wheels;
        
        [Space(5)]
        [SerializeField] private float wheelsForceMod = 1;

        private void Awake()
        {
            SetActive(false);
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
        
        public void AddForce(float force)
        {
            rb.AddForce(Vector3.up * force);
            
            foreach (var wheel in wheels)
            {
                var direction = (wheel.transform.position - transform.position).normalized;
                wheel.AddForce(direction * force * wheelsForceMod);
            }
        }
    }
}