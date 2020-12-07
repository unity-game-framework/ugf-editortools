using System;
using UGF.EditorTools.Runtime.Ids;
using UnityEditor;

namespace UGF.EditorTools.Editor.Ids
{
    public static class GlobalIdEditorUtility
    {
        public static string GetGuidFromProperty(SerializedProperty serializedProperty)
        {
            GlobalId id = GetGlobalIdFromProperty(serializedProperty);
            string guid = id.ToString();

            return guid;
        }

        public static void SetGuidToProperty(SerializedProperty serializedProperty, string guid)
        {
            if (!string.IsNullOrEmpty(guid) && GlobalId.TryParse(guid, out GlobalId id))
            {
                SetGlobalIdToProperty(serializedProperty, id);
            }
            else
            {
                SetGlobalIdToProperty(serializedProperty, GlobalId.Empty);
            }
        }

        public static GlobalId GetGlobalIdFromProperty(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            SerializedProperty propertyFirst = serializedProperty.FindPropertyRelative("m_first") ?? throw new ArgumentException("Property not found by the specified name: 'm_first'.");
            SerializedProperty propertySecond = serializedProperty.FindPropertyRelative("m_second") ?? throw new ArgumentException("Property not found by the specified name: 'm_second'.");

            ulong first = (ulong)propertyFirst.longValue;
            ulong second = (ulong)propertySecond.longValue;
            var id = new GlobalId(first, second);

            return id;
        }

        public static void SetGlobalIdToProperty(SerializedProperty serializedProperty, GlobalId id)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            SerializedProperty propertyFirst = serializedProperty.FindPropertyRelative("m_first") ?? throw new ArgumentException("Property not found by the specified name: 'm_first'.");
            SerializedProperty propertySecond = serializedProperty.FindPropertyRelative("m_second") ?? throw new ArgumentException("Property not found by the specified name: 'm_second'.");

            propertyFirst.longValue = (long)id.First;
            propertySecond.longValue = (long)id.Second;
        }
    }
}
