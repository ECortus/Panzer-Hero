using System;
using GameDevUtils.Runtime;
using GameSaveKit.Runtime;
using PanzerHero.Runtime.SavePrefs;
using PanzerHero.Runtime.Units.Player.Skins;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerSkinCollection collection;

        PanzerHeroPrefs prefs;
        
        void Start()
        {
            prefs = SaveablePrefs.LoadPrefs<PanzerHeroPrefs>();
            TrySpawnPlayer();
        }

        void TrySpawnPlayer()
        {
            var skin = collection.GetSkin(prefs.PlayerID);
            if (skin == null)
            {
                throw new ArgumentException();
            }

            var prefab = skin.Player;
            ObjectInstantiator.InstantiatePrefabForComponent(prefab, transform);
        }
    }
}