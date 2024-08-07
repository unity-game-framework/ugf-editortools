﻿using System;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.EditorTools.Editor.Serialized;
using UGF.EditorTools.Runtime.Ids;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.Assets
{
    public class AssetIdReferenceListDrawer : ReorderableListKeyAndValueDrawer
    {
        public bool DisplayAsReplace { get; set; }
        public bool DisplayReplaceButton { get; set; } = true;

        private Type m_assetType;
        private Styles m_styles;

        private class Styles
        {
            public GUIContent ReplaceButtonDisabledContent { get; } = new GUIContent(EditorGUIUtility.FindTexture("preAudioLoopOff"), "Enable replacement editing");
            public GUIContent ReplaceButtonEnabledContent { get; } = new GUIContent(EditorGUIUtility.FindTexture("preAudioLoopOff"), "Disable replacement editing");
            public Color ReplaceButtonEnableColor { get; } = new GUIStyle("PR PrefabLabel").normal.textColor;
        }

        public AssetIdReferenceListDrawer(SerializedProperty serializedProperty) : base(serializedProperty, "m_guid", "m_asset")
        {
            DisplayAsSingleLine = true;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            List.drawFooterCallback += OnDrawFooter;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            List.drawFooterCallback -= OnDrawFooter;
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyKey = serializedProperty.FindPropertyRelative(PropertyKeyName);
            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative(PropertyValueName);

            m_assetType ??= SerializedPropertyEditorUtility.GetFieldType(propertyValue);

            string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(propertyValue.objectReferenceValue));
            GlobalId idKey = propertyKey.hash128Value;
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
            AttributeEditorGUIUtility.DrawAssetHash128Field(position, serializedProperty, GUIContent.none, m_assetType);
        }

        protected override bool OnDragAndDropValidate(Object target, out Object result)
        {
            if (AssetIdEditorUtility.TryGetAssetIdReferenceTypeFromCollection(SerializedProperty, out Type type) && type.IsInstanceOfType(target))
            {
                result = target;
                return true;
            }

            result = default;
            return false;
        }

        protected override void OnDragAndDropAccept(Object target)
        {
            SerializedProperty.InsertArrayElementAtIndex(SerializedProperty.arraySize);

            SerializedProperty propertyElement = SerializedProperty.GetArrayElementAtIndex(SerializedProperty.arraySize - 1);

            AssetIdEditorUtility.SetAssetToAssetIdReference(propertyElement, target);
        }

        private void OnDrawFooter(Rect rect)
        {
            if (DisplayReplaceButton)
            {
                m_styles ??= new Styles();

                float rightEdge = rect.xMax - 10F;
                float leftEdge = rightEdge - 33F;

                if (List.displayAdd)
                {
                    leftEdge -= 25F;
                }

                if (List.displayRemove)
                {
                    leftEdge -= 25F;
                }

                var rectBackground = new Rect(leftEdge, rect.y, rightEdge - leftEdge, rect.height);
                var rectReplace = new Rect(leftEdge + 4F, rect.y, 25F, 16F);

                if (Event.current.type == EventType.Repaint)
                {
                    ReorderableList.defaultBehaviours.footerBackground.Draw(rectBackground, false, false, false, false);
                }

                try
                {
                    ReorderableList.defaultBehaviours.footerBackground.fixedHeight = float.Epsilon;
                    ReorderableList.defaultBehaviours.DrawFooter(rect, List);
                }
                finally
                {
                    ReorderableList.defaultBehaviours.footerBackground.fixedHeight = 0F;
                }

                GUIContent content = DisplayAsReplace ? m_styles.ReplaceButtonEnabledContent : m_styles.ReplaceButtonDisabledContent;
                Color color = DisplayAsReplace ? m_styles.ReplaceButtonEnableColor : Color.white;

                using (new GUIContentColorScope(color))
                {
                    DisplayAsReplace = GUI.Toggle(rectReplace, DisplayAsReplace, content, ReorderableList.defaultBehaviours.preButton);
                }
            }
            else
            {
                ReorderableList.defaultBehaviours.DrawFooter(rect, List);
            }
        }
    }
}
