using GameDevUtils.Runtime.Scene;
using PanzerHero.Runtime.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace PanzerHero.UI.MainMenu
{
    public class UMainMenu : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;
        
        private void Start()
        {
            playButton.onClick.AddListener(PlayGame);
            quitButton.onClick.AddListener(QuitGame);
        }
        
        private void PlayGame()
        {
            SceneManagement.LoadGameplayScene();
        }
        
        private void QuitGame()
        {
            SceneLoader.QuitGame();
        }
    }
}