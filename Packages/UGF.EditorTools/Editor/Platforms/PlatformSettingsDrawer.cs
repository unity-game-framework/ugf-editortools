using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Platforms
{
    public abstract class PlatformSettingsDrawer<TTarget> : DrawerBase
    {
        public List<GroupInfo> Groups { get; } = new List<GroupInfo>();

        public int SelectedGroup
        {
            get { return m_selectedGroup; }
            set
            {
                if (value < 0 || value >= Groups.Count) throw new ArgumentOutOfRangeException(nameof(SelectedGroup), "Selected group value must be in range of defined groups.");

                m_selectedGroup = value;
            }
        }

        private int m_selectedGroup;

        public class GroupInfo
        {
            public GUIContent Label { get; }
            public TTarget Target { get; }

            public GroupInfo(GUIContent label, TTarget target)
            {
                Label = label ?? throw new ArgumentNullException(nameof(label));
                Target = target;
            }
        }

        public void DrawGUILayout(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            OnDrawGUILayout(serializedProperty);
        }

        public void DrawGUI(Rect position, SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            OnDrawGUI(position, serializedProperty);
        }

        protected virtual void OnDrawGUILayout(SerializedProperty serializedProperty)
        {
        }

        protected virtual void OnDrawGUI(Rect position, SerializedProperty serializedProperty)
        {
        }

        protected virtual void OnDrawGroupToolbar(Rect position, SerializedProperty propertyGroups)
        {
        }

        protected virtual void OnDrawSettings(Rect position, SerializedProperty propertyGroup, SerializedProperty propertySettings)
        {
            EditorGUI.PropertyField(position, propertySettings);
        }

        protected virtual float OnGetSettingsHeight(SerializedProperty propertyGroup, SerializedProperty propertySettings)
        {
            return EditorGUI.GetPropertyHeight(propertySettings);
        }

        protected abstract void OnSetupGroup(SerializedProperty propertyGroup, TTarget target);
    }
}
