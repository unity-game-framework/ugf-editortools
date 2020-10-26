using System;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class ReorderableListDrawer : DrawerBase
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
                onAddCallback = OnListAdd,
                onRemoveCallback = OnListRemove,
                onSelectCallback = OnListSelect
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
            label ??= new GUIContent(SerializedProperty.displayName);

            float height = GetHeight();
            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, height);

            OnDrawGUI(position, label);
        }

        protected virtual void OnDrawGUI(Rect position, GUIContent label = null)
        {
            label ??= new GUIContent(SerializedProperty.displayName);

            var foldoutPosition = new Rect(position.x, position.y, position.width, OnGetFoldoutHeight());

            if (OnDrawFoldout(foldoutPosition, label))
            {
                float space = EditorGUIUtility.standardVerticalSpacing;
                var sizePosition = new Rect(position.x, foldoutPosition.y + foldoutPosition.height + space, position.width, OnGetSizeHeight());
                var listPosition = new Rect(position.x, sizePosition.y + sizePosition.height + space, position.width, OnGetListHeight());

                listPosition = EditorGUI.IndentedRect(listPosition);

                using (new IndentIncrementScope(1))
                {
                    OnDrawSize(sizePosition);

                    float padding = List.draggable ? ReorderableList.Defaults.dragHandleWidth : ReorderableList.Defaults.padding;

                    using (new LabelWidthChangeScope(-padding, true))
                    using (new IndentLevelScope(0))
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

            return SerializedProperty.isExpanded ? foldout + space + size + space + list : foldout;
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

            if (OnElementHasVisibleChildren(propertyElement))
            {
                position.xMin += height - space;
            }

            OnDrawElementContent(position, propertyElement, index, isActive, isFocused);
        }

        protected virtual void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            if (OnElementHasVisibleChildren(serializedProperty))
            {
                EditorGUI.PropertyField(position, serializedProperty, true);
            }
            else
            {
                if (serializedProperty.propertyType == SerializedPropertyType.ManagedReference)
                {
                    EditorGUI.PropertyField(position, serializedProperty);
                }
                else
                {
                    EditorGUI.PropertyField(position, serializedProperty, GUIContent.none);
                }
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
            return EditorGUI.GetPropertyHeight(serializedProperty, true);
        }

        protected virtual bool OnElementHasVisibleChildren(SerializedProperty serializedProperty)
        {
            return serializedProperty.hasVisibleChildren;
        }

        protected virtual void OnAdd()
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

        protected virtual void OnRemove()
        {
            SerializedProperty.DeleteArrayElementAtIndex(List.index);
            SerializedProperty.serializedObject.ApplyModifiedProperties();
        }

        protected virtual void OnSelect()
        {
        }

        private void OnListAdd(ReorderableList list)
        {
            OnAdd();
        }

        private void OnListRemove(ReorderableList list)
        {
            OnRemove();
        }

        private void OnListSelect(ReorderableList list)
        {
            OnSelect();
        }
    }
}
