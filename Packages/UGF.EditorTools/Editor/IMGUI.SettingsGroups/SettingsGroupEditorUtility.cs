using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.SettingsGroups
{
    public static class SettingsGroupEditorUtility
    {
        public static bool TrySetSettings(SerializedProperty propertyGroups, string name, object settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            if (TryGetSettings(propertyGroups, name, out SerializedProperty propertySettings))
            {
                propertySettings.managedReferenceValue = settings;
                return true;
            }

            return false;
        }

        public static bool TryGetSettings(SerializedProperty propertyGroups, string name, out SerializedProperty propertySettings)
        {
            if (TryGetGroup(propertyGroups, name, out SerializedProperty propertyGroup))
            {
                propertySettings = propertyGroup.FindPropertyRelative("m_settings");

                if (propertySettings == null) throw new ArgumentException("Found group has no 'm_settings' property.");

                return true;
            }

            propertySettings = null;
            return false;
        }

        public static bool TryClearSettings(SerializedProperty propertyGroups, string name)
        {
            if (TryGetSettings(propertyGroups, name, out SerializedProperty propertySettings))
            {
                propertySettings.managedReferenceValue = null;
                return true;
            }

            return false;
        }

        public static SerializedProperty AddGroup(SerializedProperty propertyGroups, string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

            propertyGroups.InsertArrayElementAtIndex(propertyGroups.arraySize);

            SerializedProperty propertyGroup = propertyGroups.GetArrayElementAtIndex(propertyGroups.arraySize - 1);
            SerializedProperty propertyName = propertyGroup.FindPropertyRelative("m_name");

            propertyName.stringValue = name;

            return propertyGroup;
        }

        public static bool RemoveGroup(SerializedProperty propertyGroups, string name)
        {
            for (int i = 0; i < propertyGroups.arraySize; i++)
            {
                SerializedProperty propertyGroup = propertyGroups.GetArrayElementAtIndex(i);
                SerializedProperty propertyName = propertyGroup.FindPropertyRelative("m_name");

                if (propertyName.stringValue == name)
                {
                    propertyGroups.DeleteArrayElementAtIndex(i);
                    return true;
                }
            }

            return false;
        }

        public static bool TryGetGroup(SerializedProperty propertyGroups, string name, out SerializedProperty propertyGroup)
        {
            for (int i = 0; i < propertyGroups.arraySize; i++)
            {
                propertyGroup = propertyGroups.GetArrayElementAtIndex(i);

                SerializedProperty propertyName = propertyGroup.FindPropertyRelative("m_name");

                if (propertyName.stringValue == name)
                {
                    return true;
                }
            }

            propertyGroup = null;
            return false;
        }
    }
}
