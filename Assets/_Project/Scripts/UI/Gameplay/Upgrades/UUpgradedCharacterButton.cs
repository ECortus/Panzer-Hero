using System;
using PanzerHero.Runtime.Currency;
using PanzerHero.Runtime.Units.Simultaneous;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PanzerHero.UI.Gameplay.Upgrades
{
    public class UUpgradedCharacterButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text labelText;
        [SerializeField] private TMP_Text levelText;

        [Space(5)] 
        [SerializeField] private TMP_Text previousProgressText;
        [SerializeField] private TMP_Text currentProgressText;
        [SerializeField] private TMP_Text nextProgressText;

        [Space(5)] 
        [SerializeField] private Button degradeButton;
        [SerializeField] private UProgressSlider progressSlider;
        [SerializeField] private Button upgradeButton;

        [Space(5)] 
        [SerializeField] private TMP_Text costText;

        IUpgradedCharacter character;

        CoinsManager coinsManager;

        public void SetNewCharacter(IUpgradedCharacter mainCharacter)
        {
            character = mainCharacter;
            coinsManager = CoinsManager.GetInstance;

            UpdateStaticInfo();
            UpdateDynamicInfo();

            coinsManager.onChanged += UpdateDynamicInfo;
        }

        private void OnDestroy()
        {
            if (!coinsManager)
            {
                return;
            }
            
            coinsManager.onChanged -= UpdateDynamicInfo;
        }

        void Upgrade()
        {
            character.Upgrade();
            UpdateDynamicInfo();
        }

        void Degrade()
        {
            character.Degrade();
            UpdateDynamicInfo();
        }

        void UpdateStaticInfo()
        {
            labelText.text = character.Info.Name;
            
            degradeButton.onClick.RemoveAllListeners();
            degradeButton.onClick.AddListener(Degrade);

            var stepPerProgress = character.StepCountPerProgress;
            progressSlider.SetupMaxValue(stepPerProgress);
            
            upgradeButton.onClick.RemoveAllListeners();
            upgradeButton.onClick.AddListener(Upgrade);
        }
        
        void UpdateDynamicInfo()
        {
            levelText.text = $"{character.ProgressLevel}";

            if (character.CanUpgrade)
            {
                upgradeButton.interactable = true;
            }
            else
            {
                upgradeButton.interactable = false;
            }

            var previousProgress = character.PreviousProgressValue;
            var currentProgress = character.CurrentProgressValue;
            var nextProgress = character.NextProgressValue;

            var previousDifference = MathF.Round(currentProgress - previousProgress, 2);
            var nextDifference = MathF.Round(nextProgress - currentProgress, 2);

            string previousText;
            if (character.StayOnMinProgress)
            {
                previousText = "";
            }
            else
            {
                previousText = previousDifference > 0 ? "-" : "";
                previousText += previousDifference;
            }

            string nextText;
            if (character.ReachedMaxProgress)
            {
                nextText = "";
            }
            else
            {
                nextText = nextDifference > 0 ? "+" : "";
                nextText += nextDifference;
            }

            previousProgressText.text = $"{previousText}";
            currentProgressText.text = $"{currentProgress}";
            nextProgressText.text = $"{nextText}";

            var stepLevel = character.StepLevel;
            if (character.ReachedMaxProgress)
            {
                progressSlider.SetValue(character.StepCountPerProgress);
            }
            else
            {
                progressSlider.SetValue(stepLevel);
            }

            if (character.ReachedMaxProgress)
            {
                costText.text = "...";
                costText.color = Color.white;
            }
            else
            {
                var cost = character.CurrentProgressCost;
                costText.text = $"{cost}";

                if (character.CanUpgrade)
                {
                    costText.color = Color.white;
                }
                else
                {
                    costText.color = Color.red;
                }
            }
        }
    }
}