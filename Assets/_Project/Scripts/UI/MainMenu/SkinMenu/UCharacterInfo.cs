using System;
using TMPro;
using UnityEngine;

namespace PanzerHero.UI.MainMenu.SkinMenu
{
    public class UCharacterInfo : MonoBehaviour
    {
        [SerializeField] private TMP_Text labelText;
        [SerializeField] private TMP_Text valueText;
        
        public void SetInfo(string label, float value, int valueDigits = 0)
        {
            labelText.text = label;
            
            var valueRound = MathF.Round(value, valueDigits);
            valueText.text = $"{valueRound}";
        }
    }
}