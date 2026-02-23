using GameDevUtils.Runtime;
using PanzerHero.Runtime.Currency;
using PanzerHero.Runtime.SavePrefs;
using PanzerHero.Runtime.Units.Player.Skins;
using PanzerHero.Runtime.Units.Simultaneous;
using Plugins.Tools.GameDevUtils.Runtime.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PanzerHero.UI.MainMenu.SkinMenu
{
    public class USkinButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text labelText;
        [SerializeField] private Transform modelParent;

        [Space(5)] 
        [SerializeField] private GameObject costObject;
        [SerializeField] private Button buyButton;
        [SerializeField] private TMP_Text costText;
        
        [Space(5)]
        [SerializeField] private GameObject ownedObject;
        [SerializeField] private Button equipButton;
        [SerializeField] private TMP_Text equipText;
        
        [Space(5)]
        [SerializeField] private UCharacterInfo healthInfo;
        [SerializeField] private UCharacterInfo armorInfo;
        [SerializeField] private UCharacterInfo damageInfo;
        [SerializeField] private UCharacterInfo reloadDurationInfo;

        USkinsButtonsManager manager;
        
        PlayerSkinCollection.Skin skin;

        SkinPref pref;
        
        DiamondsManager diamonds;
        
        public void SetButton(USkinsButtonsManager m, PlayerSkinCollection.Skin s)
        {
            manager = m;
            skin = s;
            
            diamonds = DiamondsManager.GetInstance;
            diamonds.onChanged += UpdateDynamicInfo;
        }
        
        void OnDestroy()
        {
            diamonds.onChanged -= UpdateDynamicInfo;
        }

        public void OnSerialize(ref PanzerHeroPrefs record)
        {
            record.SetSkinPref(skin.Id, pref);
        }

        public void OnDeserialize(PanzerHeroPrefs record)
        {
            pref = record.GetSkinPref(skin.Id);
        }

        public void UpdateStaticInfo()
        {
            var prefab = skin.Player;
            var viewModel = prefab.GetComponentInChildren<ModelView>();
            
            labelText.text = $"{prefab.name}";
            
            modelParent.DestroyAllChildren();
            var view = ObjectInstantiator.InstantiatePrefab(viewModel, modelParent);
            view.transform.localPosition = Vector3.zero;
            
            buyButton.onClick.AddListener(TryBuyMethod);
            equipButton.onClick.AddListener(TryEquipMethod);
            
            healthInfo.SetInfo("Health", pref.MaxHealthProgressLevel);
            armorInfo.SetInfo("Armor", pref.MaxArmorProgressLevel);
            damageInfo.SetInfo("Damage", pref.DamageProgressLevel);
            reloadDurationInfo.SetInfo("Reload", pref.ReloadDurationProgressLevel);
        }

        public void UpdateDynamicInfo()
        {
            if (skin.DefaultTank)
            {
                pref.IsBuyed = true;
            }
            
            bool isEquipped = false;
            isEquipped = manager.SavePrefs.PlayerID == skin.Id;
            
            if (pref.IsBuyed)
            {
                ownedObject.SetActive(true);
                costObject.SetActive(false);

                if (isEquipped)
                {
                    equipButton.interactable = false;
                    equipText.text = "Equipped";
                }
                else
                {
                    equipButton.interactable = true;
                    equipText.text = "Equip";
                }
            }
            else
            {
                ownedObject.SetActive(false);
                costObject.SetActive(true);
                
                var cost = skin.Price;
                costText.text = $"{cost}";
                
                if (diamonds.HasRequiredAmount(cost))
                {
                    buyButton.interactable = true;
                }
                else
                {
                    buyButton.interactable = false;
                }
            }
        }

        void TryBuyMethod()
        {
            var cost = skin.Price;
            if (diamonds.HasRequiredAmount(cost))
            {
                diamonds.Reduce(cost);
                pref.IsBuyed = true;
            }
            
            TryEquipMethod();
        }
        
        void TryEquipMethod()
        {
            manager.SavePrefs.SetPlayerID(skin.Id);
            manager.UpdateAllButtons();
        }
    }
}