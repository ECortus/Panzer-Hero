using System.Collections.Generic;
using GameDevUtils.Runtime;
using PanzerHero.Runtime.SavePrefs;
using PanzerHero.Runtime.Units.Player.Skins;
using Plugins.Tools.GameDevUtils.Runtime.Extensions;
using SaveableExtension.Runtime;
using SaveableExtension.Runtime.Saveable;
using UnityEngine;
using UnityEngine.UI;

namespace PanzerHero.UI.MainMenu.SkinMenu
{
    public class USkinsButtonsManager : MonoBehaviour, ISaveableBehaviour<PanzerHeroPrefs>
    {
        [SerializeField] private PlayerSkinCollection playerSkinCollection;
        
        [Space(5)]
        [SerializeField] private USkinButton buttonPrefab;
        [SerializeField] private Transform buttonsParent;

        [Space(5)] 
        [SerializeField] private bool saveOnChange = false;
        [SerializeField, DrawIf("saveOnChange", false)] private Button saveButton;

        List<USkinButton> buttons = new List<USkinButton>();

        PanzerHeroPrefs savePrefs;

        void Start()
        {
            if (!saveOnChange)
            {
                saveButton.onClick.AddListener(SavePrefsMethod);
            }
            else
            {
                saveButton.gameObject.SetActive(false);
            }
            
            InstantiateButtons();
            
            SaveableSupervisor.AddBehaviour(this);
            
            foreach (var button in buttons)
            {
                button.UpdateStaticInfo();
            }
            
            UpdateAllButtons();
        }
        
        void OnDestroy()
        {
            SaveableSupervisor.RemoveBehaviour(this);
        }
        
        void InstantiateButtons()
        {
            buttonsParent.DestroyAllChildren();
            if (buttons.Count > 0)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    ObjectHelper.Destroy(buttons[i].gameObject);
                }
                buttons.Clear();
            }

            var skins = playerSkinCollection.GetSkins();
            for (int i = 0; i < skins.Length; i++)
            {
                var skin = skins[i];
                
                var button = ObjectInstantiator.InstantiatePrefabForComponent(buttonPrefab, buttonsParent);
                button.gameObject.SetActive(true);
                
                button.SetButton(this, skin);
                buttons.Add(button);
            }
        }
        
        public void UpdateAllButtons()
        {
            foreach (var button in buttons)
            {
                button.UpdateDynamicInfo();
            }

            if (saveOnChange)
            {
                SavePrefsMethod();
            }
        }
        
        void SavePrefsMethod()
        {
            SaveablePrefs.Save<PanzerHeroPrefs>();
        }
        
        public PanzerHeroPrefs SavePrefs => savePrefs;

        public void Serialize(ref PanzerHeroPrefs record)
        {
            record.PlayerID = savePrefs.PlayerID;
            foreach (var button in buttons)
            {
                button.OnSerialize(ref record);
            }
        }
        
        public void Deserialize(PanzerHeroPrefs record)
        {
            foreach (var button in buttons)
            {
                button.OnDeserialize(record);
            }
            
            savePrefs = record;
        }
    }
}