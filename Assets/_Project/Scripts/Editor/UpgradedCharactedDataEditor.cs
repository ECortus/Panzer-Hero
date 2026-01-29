using GameDevUtils.Editor;
using PanzerHero.Runtime.Units.Simultaneous;
using UnityEditor;

namespace PanzerHero.Editor
{
    [CustomEditor(typeof(UpgradedCharactedData))]
    public class UpgradedCharactedDataEditor : CustomEditorModule
    {
        protected override bool DrawExcludedProperties => true;

        protected override void OnEditorDraw()
        {
            var stepCountPerProgress = FindProperty("stepCountPerProgress").intValue;
            var maxProgress = FindProperty("maxProgressLevel").intValue;

            var targetCountForCosts = stepCountPerProgress * maxProgress;
            
            var values = FindProperty("progressValues");
            var costs = FindProperty("progressCost");
            
            VerifyArray(values, maxProgress);
            VerifyArray(costs, maxProgress);
        }

        void VerifyArray(SerializedProperty property, int targetSize)
        {
            var size = property.arraySize;
            if (size == targetSize)
            {
                return;
            }

            if (size < targetSize)
            {
                var count = targetSize - size;
                for (int i = 0; i < count; i++)
                {
                    property.InsertArrayElementAtIndex(size - 1 + i);
                }

                return;
            }

            if (size > targetSize)
            {
                var count = size - targetSize;
                for (int i = 0; i < count; i++)
                {
                    property.DeleteArrayElementAtIndex(size - 1 - i);
                }

                return;
            }
        }
    }
}