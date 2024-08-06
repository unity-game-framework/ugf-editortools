using System;
using System.Reflection;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.FileIds
{
    public static class FileIdEditorUtility
    {
        private static readonly PropertyInfo m_inspectorMode;

        static FileIdEditorUtility()
        {
            m_inspectorMode = typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance)
                              ?? throw new ArgumentException("Property not found by the specified name: 'inspectorMode'.");
        }

        public static ulong GetFileId(Object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            using var serializedObject = new SerializedObject(target);

            m_inspectorMode.SetValue(serializedObject, InspectorMode.DebugInternal);

            SerializedProperty propertyFileId = serializedObject.FindProperty("m_LocalIdentfierInFile");

            long id = propertyFileId.longValue;

            if (id != 0)
            {
                return (ulong)id;
            }

            var globalObjectId = GlobalObjectId.GetGlobalObjectIdSlow(target);

            return globalObjectId.targetObjectId > 0 ? globalObjectId.targetObjectId : globalObjectId.targetPrefabId;
        }
    }
}
