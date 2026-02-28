using LoadingScreen.Runtime.Scene;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PanzerHero.Runtime.SceneManagement
{
    [System.Serializable]
    public class SceneManagement
    {
        public static void LoadMainMenuScene()
        {
            Time.timeScale = 1f;
            SceneLoader.LoadScene(0);
        }
        
        public static void LoadGameplayScene()
        {
            Time.timeScale = 1f;
            SceneLoader.LoadScene(1);
        }
        
        public static void ReloadScene()
        {
            Time.timeScale = 1f;
            SceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}