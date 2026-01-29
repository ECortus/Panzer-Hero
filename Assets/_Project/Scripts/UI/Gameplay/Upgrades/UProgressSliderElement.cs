using UnityEngine;

namespace PanzerHero.UI.Gameplay.Upgrades
{
    public class UProgressSliderElement : MonoBehaviour
    {
        [SerializeField] private GameObject onObj;
        [SerializeField] private GameObject offObj;

        public void Enable()
        {
            gameObject.SetActive(true);
        }
        
        public void SetOn()
        {
            onObj.SetActive(true);
            offObj.SetActive(false);
        }

        public void SetOff()
        {
            onObj.SetActive(false);
            offObj.SetActive(true);
        }
    }
}