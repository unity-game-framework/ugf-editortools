using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.References
{
    public static class ManagedReferenceEditorUtility
    {
        private static readonly char[] m_separator = { ' ' };

        public static bool TryGetType(string managedReferenceTypeName, out Type type)
        {
            if (managedReferenceTypeName == null) throw new ArgumentNullException(nameof(managedReferenceTypeName));

            if (!string.IsNullOrEmpty(managedReferenceTypeName))
            {
                string[] split = managedReferenceTypeName.Split(m_separator);
                string typeName = split.Length > 1 ? $"{split[1]}, {split[0]}" : split[0];

                type = Type.GetType(typeName);

                return type != null;
            }

            type = null;
            return false;
        }
    }
}
