using System;
using GameDevUtils.Runtime.Extensions;
using PanzerHero.Runtime.Destrictable;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Simultaneous
{
    public class VehicleCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            ProcessEnter(other.gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            ProcessEnter(other.gameObject);
        }

        void ProcessEnter(GameObject other)
        {
            if (other.IsSameMask("Destrictable"))
            {
                var idestrictable = other.GetComponent<IDestrictable>();
                idestrictable?.Destroy();
            }
        }
    }
}