using GameDevUtils.Runtime.Scene;
using UnityEngine;

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
    }
}