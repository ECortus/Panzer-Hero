using System;
using PanzerHero.Runtime.SceneManagement;
using PanzerHero.Runtime.Units;
using PanzerHero.Runtime.Units.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PanzerHero.UI.Gameplay
{
    public class ULoseScreen : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        
        [Space(5)]
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;

        UnitsManager unitsManager;

        IPlayer player => unitsManager.Player;
        
        private void Awake()
        {
            unitsManager = UnitsManager.GetInstance;
            
            restartButton.onClick.AddListener(Restart);
            exitButton.onClick.AddListener(Exit);
            
            HideRoot();
        }

        private void Update()
        {
            if (root.activeInHierarchy)
            {
                return;
            }
            
            if (!player.Health.IsAlive)
            {
                ShowRoot();
            }
        }
        
        void ShowRoot()
        {
            root.SetActive(true);
        }

        private void HideRoot()
        {
            root.SetActive(false);
        }

        void Restart()
        {
            SceneManagement.ReloadScene();
        }
        
        void Exit()
        {
            SceneManagement.LoadMainMenuScene();   
        }
    }
}