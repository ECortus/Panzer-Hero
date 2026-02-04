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
        
        Vector3 startDeadModelLocalPosition;
        
        [Serializable]
        public class Tier
        {
            [SerializeField] private GameObject HeadModel;
            [SerializeField] private GameObject BodyModel;
            [SerializeField] private GameObject GunModel;

            public void SetHead(bool isActive)
            {
                HeadModel.SetActive(isActive);
            }

            public void SetBody(bool isActive)
            {
                BodyModel.SetActive(isActive);
            }
            
            public void SetGun(bool isActive)
            {
                GunModel.SetActive(isActive);
            }
        }

        private void Awake()
        {
            startDeadModelLocalPosition = deadModel.transform.localPosition;
            SetEnabled();
        }

        public void SetEnabled()
        {
            tankModel.SetActive(true);
            deadModel.SetModelActive(false);
        }
        
        /// <summary>
        /// Set tank model tier in full view.
        /// </summary>
        /// <param name="tierNumber">Number of tier</param>
        public void SetFullTier(int tierNumber)
        {
            for (int i = 0; i < tiers.Length; i++)
            {
                var tier = tiers[i];
                
                bool areActive = false;
                if (i == tierNumber)
                {
                    areActive = true;
                }
                
                tier.SetHead(areActive);
                tier.SetBody(areActive);
                tier.SetGun(areActive);
            }
        }

        public void SetHeadTier(int tierNumber)
        {
            for (int i = 0; i < tiers.Length; i++)
            {
                var tier = tiers[i];
                
                bool isActive = false;
                if (i == tierNumber)
                {
                    isActive = true;
                }
                
                tier.SetHead(isActive);
            }
        }
        
        public void SetBodyTier(int tierNumber)
        {
            for (int i = 0; i < tiers.Length; i++)
            {
                var tier = tiers[i];
                
                bool isActive = false;
                if (i == tierNumber)
                {
                    isActive = true;
                }
                
                tier.SetBody(isActive);
            }
        }
        
        public void SetGunTier(int tierNumber)
        {
            for (int i = 0; i < tiers.Length; i++)
            {
                var tier = tiers[i];
                
                bool isActive = false;
                if (i == tierNumber)
                {
                    isActive = true;
                }
                
                tier.SetGun(isActive);
            }
        }
        
        public void SetDead()
        {
            tankModel.SetActive(false);
            
            deadModel.transform.localPosition = startDeadModelLocalPosition;
            deadModel.SetModelActive(true);
            
            deadModel.AddForce(deadModelForce);
        }
    }
}