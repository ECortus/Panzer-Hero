using System;
using PanzerHero.Runtime.Ragdoll;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player.Tank
{
    public class TankModelController : MonoBehaviour
    {
        [SerializeField] private GameObject tankModel;
        [SerializeField] private RagdollController ragdollController;
        
        [Space(5)]
        [SerializeField] private Tier[] tiers;
        
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
            ragdollController = GetComponent<RagdollController>();
            SetEnabled();
        }

        public void SetEnabled()
        {
            ragdollController.SetAsRegular();
            tankModel.SetActive(true);
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
            ragdollController.SetAsRagdoll();
        }
    }
}