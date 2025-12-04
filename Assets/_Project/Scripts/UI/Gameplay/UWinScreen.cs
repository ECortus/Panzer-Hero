using System;
using PanzerHero.Runtime.LevelDesign;
using PanzerHero.Runtime.LevelDesign.Levels;
using PanzerHero.Runtime.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace PanzerHero.UI.PauseMenu
{
    public class UWinScreen : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        
        [Space(5)]
        [SerializeField] private Button continueButton;
        [SerializeField] private Button goToMenuButton;
        
        private void Start()
        {
            var statement = GameStatement.GetInstance;
            statement.OnGameFinished += ShowWinScreen;
            
            continueButton.onClick.AddListener(ContinueGame);
            goToMenuButton.onClick.AddListener(GoToMenu);
        }
        
        private void ShowWinScreen()
        {
            root.SetActive(true);
        }
        
        private void ContinueGame()
        {
            var statement = GameStatement.GetInstance;
            statement.GoNextGame();
            
            root.SetActive(false);
        }
        
        private void GoToMenu()
        {
            var collector = LevelsCollector.GetInstance;
            collector.SetNextID();
            
            SceneLoader.LoadScene(0);
        }
    }
}