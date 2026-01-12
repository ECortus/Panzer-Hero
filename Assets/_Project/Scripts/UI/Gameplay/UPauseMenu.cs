using GameDevUtils.Runtime.Scene;
using GameSaveKit.Runtime;
using PanzerHero.Runtime.SavePrefs;
using PanzerHero.Runtime.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace PanzerHero.UI.Gameplay
{
    public class UPauseMenu : MonoBehaviour
    {
        [SerializeField] private Button pauseButton;

        [Space(5)]
        [SerializeField] private GameObject root;
        
        [Space(5)]
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button goToMenuButton;
        [SerializeField] private Button quitButton;
        
        private void Start()
        {
            pauseButton.onClick.AddListener(PauseGame);
            
            resumeButton.onClick.AddListener(ResumeGame);
            saveButton.onClick.AddListener(SaveGame);
            
            goToMenuButton.onClick.AddListener(GoToMenu);
            quitButton.onClick.AddListener(QuitGame);
        }
        
        private void PauseGame()
        {
            root.SetActive(true);
            Time.timeScale = 0;
        }
        
        private void ResumeGame()
        {
            root.SetActive(false);
            Time.timeScale = 1;
        }
        
        private void SaveGame()
        {
            SaveablePrefs.Save<PanzerHeroPrefs>();
        }
        
        private void GoToMenu()
        {
            SceneManagement.LoadMainMenuScene();
        }
        
        private void QuitGame()
        {
            SceneLoader.QuitGame();
        }
    }
}