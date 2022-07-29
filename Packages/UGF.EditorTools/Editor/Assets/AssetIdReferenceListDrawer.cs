using System;
using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UGF.EditorTools.Editor.Serialized;
using UGF.EditorTools.Runtime.Ids;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Assets
{
    public class AssetIdReferenceListDrawer : ReorderableListKeyAndValueDrawer
    {
        public bool DisplayAsReplace { get; set; }

        private Type m_assetType;

        public AssetIdReferenceListDrawer(SerializedProperty serializedProperty) : base(serializedProperty, "m_guid", "m_asset")
        {
            DisplayAsSingleLine = true;
        }

        public void DrawReplaceControlsLayout()
        {
            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                DrawReplaceButtonLayout();
            }
        }

        public void DrawReplaceButtonLayout()
        {
            DisplayAsReplace = GUILayout.Toggle(DisplayAsReplace, "Replace", EditorStyles.miniButton);
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyKey = serializedProperty.FindPropertyRelative(PropertyKeyName);
            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative(PropertyValueName);

            m_assetType ??= SerializedPropertyEditorUtility.GetFieldType(propertyValue);

            string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(propertyValue.objectReferenceValue));
            GlobalId idKey = GlobalIdEditorUtility.GetGlobalIdFromProperty(propertyKey);
            GlobalId idAsset = !string.IsNullOrEmpty(guid) && GlobalId.TryParse(guid, out GlobalId result) ? result : GlobalId.Empty;

            if ((idKey != GlobalId.Empty && idKey != idAsset) || DisplayAsReplace)
            {
                base.OnDrawElementContent(position, serializedProperty, index, isActive, isFocused);
            }
            else
            {
                AssetIdReferenceEditorGUIUtility.AssetIdReferenceField(position, GUIContent.none, serializedProperty);
            }
        }

        protected override void OnDrawKey(Rect position, SerializedProperty serializedProperty)
        {
            string guid = GlobalIdEditorUtility.GetGuidFromProperty(serializedProperty);

            guid = AttributeEditorGUIUtility.DrawAssetGuidField(position, guid, GUIContent.none, m_assetType);

            GlobalIdEditorUtility.SetGuidToProperty(serializedProperty, guid);
        }
    }
}
