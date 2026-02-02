using System;
using GameDevUtils.Runtime;
using PanzerHero.Runtime.Units.Player;
using PanzerHero.Runtime.Units.Player.Components;
using UnityEngine;
using UnityEngine.UI;

namespace PanzerHero.UI.Testing
{
    public class UDestroyPlayer : MonoBehaviour
    {
        private void Start()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                var player = FindAnyObjectByType<PlayerHeader>();
                if (player)
                {
                    ObjectHelper.Destroy(player.gameObject);
                }
            });
        }
    }
}