using System;
using GameDevUtils.Runtime;
using GameSaveKit.Runtime.Saveable;
using PanzerHero.Runtime.SavePrefs;
using PanzerHero.Runtime.Units.Player.Skins;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player
{
    public class PlayerSpawner : MonoBehaviour, ISaveableBehaviour<PanzerHeroPrefs>
    {
        [SerializeField] private PlayerSkinCollection collection;

        int PlayerID = -1;
        
        void Start()
        {
            SaveableSupervisor.AddBehaviour(this);
            TrySpawnPlayer();
        }

        void TrySpawnPlayer()
        {
            var skin = collection.GetSkin(PlayerID);
            if (skin == null)
            {
                throw new ArgumentException();
            }

            var prefab = skin.Player;
            ObjectInstantiator.InstantiatePrefabForComponent(prefab, transform);
        }

        public void Serialize(ref PanzerHeroPrefs record)
        {
            
        }

        public void Deserialize(PanzerHeroPrefs record)
        {
            PlayerID = record.PlayerID;
        }
    }
}