using UnityEngine;
using UnityEngine.SceneManagement;

namespace PanzerHero.Runtime.SceneManagement
{
    [System.Serializable]
    public class SceneLoader
    {
        public static AsyncOperation LoadScene(int index)
        {
            return SceneManager.LoadSceneAsync(index);
        }
        
        public static AsyncOperation LoadScene(string name)
        {
            return SceneManager.LoadSceneAsync(name);
        }

        public static void QuitGame()
        {
            Application.Quit();
        }
    }
}