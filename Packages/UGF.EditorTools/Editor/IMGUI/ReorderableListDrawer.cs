using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class ReorderableListDrawer
    {
        public SerializedProperty SerializedProperty { get; }
        public ReorderableList List { get; }

        public ReorderableListDrawer(SerializedProperty serializedProperty)
        {
            SerializedProperty = serializedProperty ?? throw new ArgumentNullException(nameof(serializedProperty));
            List = new ReorderableList(serializedProperty.serializedObject, serializedProperty)
            {
                drawHeaderCallback = OnDrawHeader,
                drawElementCallback = OnDrawElement,
                elementHeightCallback = OnElementHeight,
                onAddCallback = OnAdd,
                onRemoveCallback = OnRemove
            };
        }

        public void DrawGUILayout()
        {
            List.DoLayoutList();
        }

        public void DrawGUI(Rect position)
        {
            List.DoList(position);
        }

        protected virtual void OnDrawHeader(Rect position)
        {
            GUI.Label(position, $"{SerializedProperty.displayName} (Size: {SerializedProperty.arraySize})", EditorStyles.boldLabel);
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
