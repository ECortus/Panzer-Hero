using System;
using PanzerHero.Runtime.LevelDesign;
using PanzerHero.Runtime.LevelDesign.Levels;
using PanzerHero.Runtime.LevelDesign.Rewards;
using PanzerHero.Runtime.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PanzerHero.UI.Gameplay
{
    public class UWinScreen : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        
        [Space(5)]
        [SerializeField] private Button continueButton;
        [SerializeField] private Button goToMenuButton;
        
        [Space(5)]
        [SerializeField] private TMP_Text coinsText;
        [SerializeField] private TMP_Text diamondsText;

        RewardManager rewardManager;
        
        private void Start()
        {
            var statement = GameStatement.GetInstance;
            statement.OnGameFinished += ShowRoot;

            rewardManager = RewardManager.GetInstance;
            
            continueButton.onClick.AddListener(ContinueGame);
            goToMenuButton.onClick.AddListener(GoToMenu);
        }
        
        private void ShowRoot()
        {
            root.SetActive(true);

            var coins = rewardManager.GetRewardedCoinsValue();
            coinsText.text = $"{coins}";
            
            var diamonds = rewardManager.GetRewardedDiamondsValue();
            diamondsText.text = $"{diamonds}";
        }
        
        private void HideRoot()
        {
            root.SetActive(false);
        }
        
        private void ContinueGame()
        {
            var manager = LevelManager.GetInstance;
            manager.SetNextLevel();
            
            var statement = GameStatement.GetInstance;
            statement.RelaunchGame();

            HideRoot();
        }
        
        private void GoToMenu()
        {
            var collector = LevelManager.GetInstance;
            collector.SetNextID();

            SceneManagement.LoadMainMenuScene();
        }
    }
}