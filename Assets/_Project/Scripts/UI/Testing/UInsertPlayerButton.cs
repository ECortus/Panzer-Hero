using Cysharp.Threading.Tasks;
using GameDevUtils.Runtime;
using PanzerHero.Runtime.Units.Player;
using UnityEngine;
using UnityEngine.UI;

namespace PanzerHero.UI.Testing
{
    public class UInsertPlayerButton : MonoBehaviour
    {
        [SerializeField] private PlayerHeader playerPrefab;

        void Start()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(InsertPlayer);
        }
        
        async void InsertPlayer()
        {
            var oldPlayer = FindAnyObjectByType<PlayerHeader>();
            if (oldPlayer)
            {
                var oldParent = oldPlayer.transform.parent;
            
                ObjectHelper.Destroy(oldPlayer.gameObject);
            
                await UniTask.Yield();
                await UniTask.Yield();
                await UniTask.Yield();
            
                ObjectInstantiator.InstantiatePrefab(playerPrefab, oldParent);
            }
            else
            {
                await UniTask.Yield();
                await UniTask.Yield();
                await UniTask.Yield();
            
                ObjectInstantiator.InstantiatePrefab(playerPrefab);
            }
        }
    }
}