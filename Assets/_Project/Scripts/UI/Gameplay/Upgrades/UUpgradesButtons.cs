using System;
using GameDevUtils.Runtime;
using PanzerHero.Runtime.Units;
using PanzerHero.Runtime.Units.Interfaces;
using PanzerHero.Runtime.Units.Player.Components;
using PanzerHero.Runtime.Units.Simultaneous;
using Plugins.Tools.GameDevUtils.Runtime.Extensions;
using UnityEngine;
using Object = System.Object;

namespace PanzerHero.UI.Gameplay.Upgrades
{
    public class UUpgradesButtons : MonoBehaviour
    {
        UnitsManager unitsManager;

        IPlayer player;
        
        [SerializeField] private UUpgradedCharacterButton buttonPrefab;
        [SerializeField] private Transform buttonsRoot;

        bool inserted;
        
        private void Start()
        {
            unitsManager = UnitsManager.GetInstance;
            DeleteButtons();
        }

        private void Update()
        {
            player = unitsManager.Player;
            
            if (player == null)
            {
                if (buttonsRoot.childCount > 0)
                {
                    DeleteButtons();
                }
                
                inserted = false;
                return;
            }

            if (!inserted)
            {
                var characters = player.UpgradedCharacters;
                InsertNewButtons(characters);
                
                inserted = true;
            }
        }

        void InsertNewButtons(IPlayerUpgradedCharacters characters)
        {
            var health = characters.MaxHealth;
            var armor = characters.MaxArmor;
            var damage = characters.Damage;
            var reload = characters.ReloadDuration;
            
            InsertNewButton(health);
            InsertNewButton(armor);
            InsertNewButton(damage);
            InsertNewButton(reload);
        }

        void InsertNewButton(IUpgradedCharacter character)
        {
            var obj = ObjectInstantiator.InstantiatePrefabForComponent(buttonPrefab, buttonsRoot);
            obj.SetNewCharacter(character);
        }

        void DeleteButtons()
        {
            buttonsRoot.DestroyAllChildren();
        }
    }
}