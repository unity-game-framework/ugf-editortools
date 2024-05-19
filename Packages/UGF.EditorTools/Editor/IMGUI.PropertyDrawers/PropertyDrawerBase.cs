using System;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.PropertyDrawers
{
    public abstract class PropertyDrawerBase : PropertyDrawer, IDisposable
    {
        protected bool EnableContextMenu { get; set; }

        private readonly EditorApplication.SerializedPropertyCallbackFunction m_contextMenuHandler;

        protected PropertyDrawerBase()
        {
            m_contextMenuHandler = OnContextMenu;
        }

        public override void OnGUI(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            if (EnableContextMenu)
            {
                using (new SerializedPropertyContextMenuScope(m_contextMenuHandler))
                using (new SerializedPropertyGUIScope(position, label, serializedProperty))
                {
                    OnGUIProperty(position, serializedProperty, label);
                }
            }
            else
            {
                using (new SerializedPropertyGUIScope(position, label, serializedProperty))
                {
                    OnGUIProperty(position, serializedProperty, label);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(serializedProperty, label, true);
        }

        protected virtual void OnGUIProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            OnDrawProperty(position, serializedProperty, label);
        }

        protected abstract void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label);

        protected virtual void OnDrawPropertyDefault(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            EditorGUI.PropertyField(position, serializedProperty, label, true);
        }

        protected virtual void OnContextMenu(GenericMenu menu, SerializedProperty property)
        {
        }

        protected virtual void OnDispose()
        {
        }

        void IDisposable.Dispose()
        {
            OnDispose();
        }
    }
}
