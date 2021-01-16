using System;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class CollectionDrawer : DrawerBase
    {
        public SerializedProperty SerializedProperty { get; }
        public SerializedProperty PropertySize { get; }

        public CollectionDrawer(SerializedProperty serializedProperty)
        {
            SerializedProperty = serializedProperty ?? throw new ArgumentNullException(nameof(serializedProperty));
            PropertySize = SerializedProperty.FindPropertyRelative("Array.size");
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
                var sizePosition = new Rect(position.x, foldoutPosition.yMax + space, position.width, OnGetSizeHeight());
                var collectionPosition = new Rect(position.x, sizePosition.yMax + space, position.width, OnGetCollectionHeight());

                using (new IndentIncrementScope(1))
                {
                    OnDrawSize(sizePosition);
                    OnDrawCollection(collectionPosition);
                }
            }
        }

        protected virtual float OnGetHeight()
        {
            float space = EditorGUIUtility.standardVerticalSpacing;
            float foldout = OnGetFoldoutHeight();
            float size = OnGetSizeHeight();
            float list = OnGetCollectionHeight();

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

        protected virtual void OnDrawCollection(Rect position)
        {
            for (int i = 0; i < SerializedProperty.arraySize; i++)
            {
                float height = OnElementHeight(i);

                position.height = height;

                OnDrawElement(position, i);

                position.y += height;
            }
        }

        protected virtual float OnGetCollectionHeight()
        {
            float height = 0F;

            for (int i = 0; i < SerializedProperty.arraySize; i++)
            {
                float elementHeight = OnElementHeight(i);

                height += elementHeight;
            }

            return height;
        }

        protected virtual void OnDrawElement(Rect position, int index)
        {
            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            position.height = height;
            position.y += space;

            SerializedProperty propertyElement = SerializedProperty.GetArrayElementAtIndex(index);

            OnDrawElementContent(position, propertyElement, index);
        }

        protected virtual void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index)
        {
            EditorGUI.PropertyField(position, serializedProperty, true);
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
    }
}
