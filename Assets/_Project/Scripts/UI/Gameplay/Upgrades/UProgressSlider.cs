using System;
using System.Collections.Generic;
using GameDevUtils.Runtime;
using Plugins.Tools.GameDevUtils.Runtime.Extensions;
using UnityEngine;

namespace PanzerHero.UI.Gameplay.Upgrades
{
    public class UProgressSlider : MonoBehaviour
    {
        RectTransform rect;
        
        [SerializeField] private float sizePerEachElement = 30f;
        
        [Space(5)]
        [SerializeField] private UProgressSliderElement elementPrefab;
        [SerializeField] private Transform parent;

        int maxCount;

        List<UProgressSliderElement> elements = new List<UProgressSliderElement>();
        
        public void SetupMaxValue(int value)
        {
            rect ??= GetComponent<RectTransform>();
            
            ResetSlider();
            
            maxCount = value;
            RefreshSlider();
            
            SetValue(0);
        }

        void ResetSlider()
        {
            parent.DestroyAllChildren();
            elements = new List<UProgressSliderElement>();
        }

        public void SetValue(int value)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                var element = elements[i];
                if (value > i)
                {
                    element.SetOn();
                }
                else
                {
                    element.SetOff();
                }
            }
        }

        void RefreshSlider()
        {
            rect.sizeDelta = new Vector2(sizePerEachElement * maxCount, sizePerEachElement * maxCount);
            
            int countInParent = parent.childCount;
            if (maxCount != countInParent)
            {
                if (maxCount > countInParent)
                {
                    int toSpawn = maxCount - countInParent;
                    InsertNewElements(toSpawn);
                }
                else
                {
                    int toDelete = countInParent - maxCount;
                    DeleteElements(toDelete);
                }
            }
        }

        void InsertNewElements(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var obj = ObjectInstantiator.InstantiatePrefabForComponent(elementPrefab, parent);
                obj.SetOn();

                obj.Enable();
                elements.Add(obj);
            }
        }

        void DeleteElements(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var obj = elements[0];
                elements.RemoveAt(0);
                
                ObjectHelper.Destroy(obj.gameObject, true);
            }
        }
    }
}