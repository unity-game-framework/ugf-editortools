using System;
using UGF.EditorTools.Runtime.Platforms;
using UnityEditor;

namespace UGF.EditorTools.Editor.Platforms
{
    public static class PlatformEditorUtility
    {
        public static bool TrySetSettings<T>(SerializedProperty propertyGroups, T target, IPlatformSettings settings, PlatformGroupTargetCompareHandler<T> compareHandler)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            if (TryGetSettings(propertyGroups, target, out SerializedProperty propertySettings, compareHandler))
            {
                propertySettings.managedReferenceValue = settings;
                return true;
            }

            return false;
        }

        public static bool TryGetSettings<T>(SerializedProperty propertyGroups, T target, out SerializedProperty propertySettings, PlatformGroupTargetCompareHandler<T> compareHandler)
        {
            if (TryGetGroup(propertyGroups, target, out SerializedProperty propertyGroup, compareHandler))
            {
                propertySettings = propertyGroup.FindPropertyRelative("m_settings");
                return true;
            }

            propertySettings = null;
            return false;
        }

        public static bool TryClearSettings<T>(SerializedProperty propertyGroups, T target, PlatformGroupTargetCompareHandler<T> compareHandler)
        {
            if (TryGetSettings(propertyGroups, target, out SerializedProperty propertySettings, compareHandler))
            {
                propertySettings.managedReferenceValue = null;
                return true;
            }

            return false;
        }

        public static bool TryGetGroup<T>(SerializedProperty propertyGroups, T target, out SerializedProperty propertyGroup, PlatformGroupTargetCompareHandler<T> compareHandler)
        {
            if (compareHandler == null) throw new ArgumentNullException(nameof(compareHandler));

            for (int i = 0; i < propertyGroups.arraySize; i++)
            {
                propertyGroup = propertyGroups.GetArrayElementAtIndex(i);

                if (compareHandler(propertyGroup, target))
                {
                    return true;
                }
            }

            propertyGroup = null;
            return false;
        }
    }
}
