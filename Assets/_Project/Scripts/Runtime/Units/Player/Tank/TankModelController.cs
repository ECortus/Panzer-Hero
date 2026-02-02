using System;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player.Tank
{
    public class TankModelController : MonoBehaviour
    {
        [SerializeField] private GameObject tankModel;
        
        [Space(5)]
        [SerializeField] private Tier[] tiers;
        
        [Space(5)]
        [SerializeField] private DeadTankModelController deadModel;
        [SerializeField] private float deadModelForce = 15;
        
        Vector3 startDeadModelPosition;
        
        [Serializable]
        public struct Tier
        {
            public GameObject HeadModel;
            public GameObject BodyModel;
            public GameObject GunModel;
        }

        private void Awake()
        {
            startDeadModelPosition = deadModel.transform.position;
        }

        public void SetEnabled()
        {
            tankModel.SetActive(true);
            deadModel.SetActive(false);
        }

        /// <summary>
        /// Set tank model tier.
        /// </summary>
        /// <param name="tierNumber">Number of tier (1-3)</param>
        /// <param name="partNumber">Part number (1 - head, 2 - body, 3 - gun)</param>
        public void SetTier(int tierNumber, int partNumber)
        {
            for (int i = 0; i < tiers.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == tierNumber)
                    {
                        tiers[i].HeadModel.SetActive(partNumber == 1);
                        tiers[i].BodyModel.SetActive(partNumber == 2);
                        tiers[i].GunModel.SetActive(partNumber == 3);
                    }
                    else
                    {
                        tiers[i].HeadModel.SetActive(false);
                        tiers[i].BodyModel.SetActive(false);
                        tiers[i].GunModel.SetActive(false);
                    }
                }   
            }
        }
        
        public void SetDead()
        {
            deadModel.transform.position = startDeadModelPosition;
            
            tankModel.SetActive(false);
            deadModel.SetActive(true);
            
            AddForceToDeadModel();
        }
        
        void AddForceToDeadModel()
        {
            deadModel.AddForce(deadModelForce);
        }
    }
}