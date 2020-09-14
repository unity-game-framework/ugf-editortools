using System;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class ReorderableListDrawer
    {
        public SerializedProperty SerializedProperty { get; }
        public SerializedProperty PropertySize { get; }
        public ReorderableList List { get; }

        public ReorderableListDrawer(SerializedProperty serializedProperty)
        {
            SerializedProperty = serializedProperty ?? throw new ArgumentNullException(nameof(serializedProperty));
            PropertySize = SerializedProperty.FindPropertyRelative("Array.size");
            List = new ReorderableList(serializedProperty.serializedObject, serializedProperty)
            {
                headerHeight = EditorGUIUtility.standardVerticalSpacing,
                drawHeaderCallback = OnDrawHeader,
                drawElementCallback = OnDrawElement,
                elementHeightCallback = OnElementHeight,
                onAddCallback = OnAdd,
                onRemoveCallback = OnRemove
            };
        }

        public void DrawGUILayout()
        {
            OnDrawGUILayout();
        }

        public void DrawGUI(Rect position)
        {
            OnDrawGUI(position);
        }

        public float GetHeight()
        {
            return OnGetHeight();
        }

        protected virtual void OnDrawGUILayout(GUIContent label = null)
        {
            if (label == null) label = new GUIContent(SerializedProperty.displayName);

            float height = GetHeight();
            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, height);

            OnDrawGUI(position, label);
        }

        protected virtual void OnDrawGUI(Rect position, GUIContent label = null)
        {
            if (label == null) label = new GUIContent(SerializedProperty.displayName);

            var foldoutPosition = new Rect(position.x, position.y, position.width, OnGetFoldoutHeight());

            if (OnDrawFoldout(foldoutPosition, label))
            {
                float space = EditorGUIUtility.standardVerticalSpacing;
                var sizePosition = new Rect(position.x, foldoutPosition.y + foldoutPosition.height + space, position.width, OnGetSizeHeight());
                var listPosition = new Rect(position.x, sizePosition.y + sizePosition.height + space, position.width, OnGetListHeight());

                listPosition = EditorGUI.IndentedRect(listPosition);

                using (new EditorGUI.IndentLevelScope())
                {
                    OnDrawSize(sizePosition);

                    float indentWidth = EditorIMGUIUtility.GetIndent();
                    float labelWidth = EditorGUIUtility.labelWidth;
                    float padding = ReorderableList.Defaults.dragHandleWidth;

                    using (new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel))
                    using (new LabelWidthScope(labelWidth - indentWidth - padding))
                    {
                        List.DoList(listPosition);
                    }
                }
            }
        }

        protected virtual float OnGetHeight()
        {
            float space = EditorGUIUtility.standardVerticalSpacing;
            float foldout = OnGetFoldoutHeight();
            float size = OnGetSizeHeight();
            float list = OnGetListHeight();

            return foldout + space + size + space + list;
        }

        protected virtual bool OnDrawFoldout(Rect position, GUIContent label)
        {
            SerializedProperty.isExpanded = EditorGUI.Foldout(position, SerializedProperty.isExpanded, label, true);

            return SerializedProperty.isExpanded;
        }

        protected virtual float OnGetFoldoutHeight()
        {
            return EditorGUIUtility.singleLineHeight;
        }

        protected virtual void OnDrawSize(Rect position)
        {
            EditorGUI.PropertyField(position, PropertySize);
        }

        protected virtual float OnGetSizeHeight()
        {
            return EditorGUIUtility.singleLineHeight;
        }

        protected virtual float OnGetListHeight()
        {
            return List.GetHeight();
        }

        protected virtual void OnDrawHeader(Rect position)
        {
        }

        protected virtual void OnDrawElement(Rect position, int index, bool isActive, bool isFocused)
        {
            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            position.height = height;
            position.y += space;

            SerializedProperty propertyElement = SerializedProperty.GetArrayElementAtIndex(index);

            if (propertyElement.hasVisibleChildren)
            {
                position.xMin += height - space;
            }

            OnDrawElementContent(position, propertyElement, index, isActive, isFocused);
        }

        protected virtual void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            if (serializedProperty.propertyType == SerializedPropertyType.ObjectReference)
            {
                EditorGUI.PropertyField(position, serializedProperty, GUIContent.none);
            }
            else
            {
                EditorGUI.PropertyField(position, serializedProperty, true);
            }
        }

        protected virtual float OnElementHeight(int index)
        {
            SerializedProperty propertyElement = SerializedProperty.GetArrayElementAtIndex(index);

            float value = OnElementHeightContent(propertyElement, index);

            value += EditorGUIUtility.standardVerticalSpacing * 2F;

            return value;
        }

        protected virtual float OnElementHeightContent(SerializedProperty serializedProperty, int index)
        {
            return serializedProperty.propertyType == SerializedPropertyType.ObjectReference
                ? EditorGUIUtility.singleLineHeight
                : EditorGUI.GetPropertyHeight(serializedProperty, true);
        }

        protected virtual void OnAdd(ReorderableList list)
        {
            SerializedProperty.InsertArrayElementAtIndex(SerializedProperty.arraySize);

            SerializedProperty propertyElement = SerializedProperty.GetArrayElementAtIndex(SerializedProperty.arraySize - 1);

            switch (propertyElement.propertyType)
            {
                case SerializedPropertyType.ObjectReference:
                {
                    propertyElement.objectReferenceValue = null;
                    break;
                }
                case SerializedPropertyType.ManagedReference:
                {
                    propertyElement.managedReferenceValue = null;
                    break;
                }
            }

            propertyElement.serializedObject.ApplyModifiedProperties();
        }

        protected virtual void OnRemove(ReorderableList list)
        {
            SerializedProperty.DeleteArrayElementAtIndex(list.index);
            SerializedProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}
