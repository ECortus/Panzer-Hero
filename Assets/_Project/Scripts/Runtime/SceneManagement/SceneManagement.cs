using UnityEngine;

namespace PanzerHero.Runtime.SceneManagement
{
    [System.Serializable]
    public class SceneManagement
    {
        public static AsyncOperation LoadMainMenuScene()
        {
            Time.timeScale = 1f;
            return SceneLoader.LoadScene(0);
        }
        
        public static AsyncOperation LoadGameplayScene()
        {
            Time.timeScale = 1f;
            return SceneLoader.LoadScene(1);
        }
    }
}