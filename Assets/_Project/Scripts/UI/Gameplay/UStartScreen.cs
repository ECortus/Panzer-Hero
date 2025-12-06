using PanzerHero.Runtime.LevelDesign;
using UnityEngine;
using UnityEngine.UI;

namespace PanzerHero.UI.Gameplay
{
    public class UStartScreen : MonoBehaviour
    {
        [SerializeField] private Button startButton;

        [Space(5)] 
        [SerializeField] private GameObject root;
        
        void Awake()
        {
            startButton.onClick.AddListener(StartGame);
            ShowRoot();
            
            var statement = GameStatement.GetInstance;
            statement.OnGameLaunched += ShowRoot;
        }

        void ShowRoot()
        {
            root.SetActive(true);
        }

        void HideRoot()
        {
            root.SetActive(false);
        }
        
        void StartGame()
        {
            var statement = GameStatement.GetInstance;
            statement.StartGame();
            
            HideRoot();
        }
    }
}