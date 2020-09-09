﻿using System;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.PropertyDrawers
{
    public abstract class PropertyDrawer<TAttribute> : PropertyDrawer where TAttribute : PropertyAttribute
    {
        public TAttribute Attribute { get { return (TAttribute)attribute; } }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                try
                {
                    OnGUIProperty(position, property, label);
                }
                catch (Exception exception)
                {
                    OnDrawPropertyDefault(position, property, label);

                    Debug.LogError($"Exception while drawing property: '{property.propertyPath}'.\nException: {exception}");
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        protected virtual void OnGUIProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            OnDrawProperty(position, property, label);
        }

        protected abstract void OnDrawProperty(Rect position, SerializedProperty property, GUIContent label);

        protected virtual void OnDrawPropertyDefault(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}