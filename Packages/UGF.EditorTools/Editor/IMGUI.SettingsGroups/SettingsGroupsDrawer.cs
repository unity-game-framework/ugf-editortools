using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.SettingsGroups
{
    public class SettingsGroupsDrawer : DrawerBase
    {
        public IReadOnlyList<string> Groups { get; }
        public SettingsGroupsToolbarDrawer Toolbar { get; set; } = new SettingsGroupsToolbarDrawer();
        public bool AllowEmptySettings { get; set; } = true;

        private readonly List<string> m_groups = new List<string>();
        private Styles m_styles;
        private static float m_padding = 5F;

        private class Styles
        {
            public GUIStyle FrameBox { get; } = new GUIStyle("FrameBox");
        }

        public SettingsGroupsDrawer()
        {
            Groups = new ReadOnlyCollection<string>(m_groups);
        }

        public void AddGroup(string name, GUIContent label)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            if (label == null) throw new ArgumentNullException(nameof(label));

            m_groups.Add(name);
            Toolbar.AddLabel(label);
        }

        public bool RemoveGroup(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

            int index = m_groups.IndexOf(name);

            if (index >= 0 || index < Groups.Count)
            {
                m_groups.RemoveAt(index);
                Toolbar.RemoveLabel(Toolbar.TabLabels[index]);
                return true;
            }

            return false;
        }

        public void ClearGroups()
        {
            m_groups.Clear();
            Toolbar.ClearLabels();
        }

        public void DrawGUILayout(SerializedProperty propertyGroups)
        {
            if (propertyGroups == null) throw new ArgumentNullException(nameof(propertyGroups));

            float height = OnGetHeight(propertyGroups);
            Rect position = EditorGUILayout.GetControlRect(false, height);

            DrawGUI(position, propertyGroups);
        }

        public void DrawGUI(Rect position, SerializedProperty propertyGroups)
        {
            if (propertyGroups == null) throw new ArgumentNullException(nameof(propertyGroups));
            if (m_styles == null) m_styles = new Styles();

            OnDrawGUI(position, propertyGroups);
        }

        public float GetHeight(SerializedProperty propertyGroups)
        {
            return OnGetHeight(propertyGroups);
        }

        protected virtual void OnDrawGUI(Rect position, SerializedProperty propertyGroups)
        {
            if (Event.current.type == EventType.Repaint)
            {
                m_styles.FrameBox.Draw(position, false, false, false, false);
            }

            float toolbarHeight = OnGetToolbarHeight(propertyGroups);
            var toolbarPosition = new Rect(position.x, position.y, position.width, toolbarHeight);

            float settingsHeight = OnGetSettingsHeight(propertyGroups);
            var settingsPosition = new Rect(position.x, toolbarPosition.yMax, position.width, settingsHeight);

            settingsPosition.xMin += m_padding;
            settingsPosition.xMax -= m_padding;
            settingsPosition.yMin += m_padding;
            settingsPosition.yMax -= m_padding;

            OnDrawToolbar(toolbarPosition, propertyGroups);
            OnDrawSettings(settingsPosition, propertyGroups);
        }

        protected virtual void OnDrawToolbar(Rect position, SerializedProperty propertyGroups)
        {
            Toolbar.DrawGUI(position);
        }

        protected virtual void OnDrawSettings(Rect position, SerializedProperty propertyGroups)
        {
            string name = GetSelectedGroupName();

            OnDrawSettings(position, propertyGroups, name);
        }

        protected virtual void OnDrawSettings(Rect position, SerializedProperty propertyGroups, string name)
        {
            SerializedProperty propertySettings = OnGetSettings(propertyGroups, name);

            using (new IndentIncrementScope(1))
            using (new LabelWidthChangeScope(-m_padding))
            {
                EditorGUI.PropertyField(position, propertySettings, true);
            }
        }

        protected virtual SerializedProperty OnGetSettings(SerializedProperty propertyGroups, string name)
        {
            if (!SettingsGroupEditorUtility.TryGetSettings(propertyGroups, name, out SerializedProperty propertySettings))
            {
                if (!SettingsGroupEditorUtility.TryGetGroup(propertyGroups, name, out SerializedProperty propertyGroup))
                {
                    propertyGroup = SettingsGroupEditorUtility.AddGroup(propertyGroups, name);
                    propertyGroup.serializedObject.ApplyModifiedProperties();
                }

                propertySettings = propertyGroup.FindPropertyRelative("m_settings");

                OnCreateSettings(propertyGroups, name, propertySettings);
            }

            if (!AllowEmptySettings && string.IsNullOrEmpty(propertySettings.managedReferenceFullTypename))
            {
                OnCreateSettings(propertyGroups, name, propertySettings);
            }

            return propertySettings;
        }

        protected virtual void OnCreateSettings(SerializedProperty propertyGroups, string name, SerializedProperty propertySettings)
        {
            propertySettings.managedReferenceValue = null;
        }

        protected virtual string GetSelectedGroupName()
        {
            if (Groups.Count == 0) throw new ArgumentException("Settings groups not specified.");
            if (Toolbar.Selected < 0 || Toolbar.Selected >= Groups.Count) throw new ArgumentOutOfRangeException(nameof(Toolbar.Selected), $"Settings group not found by the specified index: '{Toolbar.Selected}'.");

            return Groups[Toolbar.Selected];
        }

        protected virtual float OnGetHeight(SerializedProperty propertyGroups)
        {
            float toolbar = OnGetToolbarHeight(propertyGroups);
            float settings = OnGetSettingsHeight(propertyGroups);

            return toolbar + settings;
        }

        protected virtual float OnGetToolbarHeight(SerializedProperty propertyGroups)
        {
            return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 3F;
        }

        protected virtual float OnGetSettingsHeight(SerializedProperty propertyGroups)
        {
            string name = GetSelectedGroupName();
            SerializedProperty propertySettings = OnGetSettings(propertyGroups, name);

            return EditorGUI.GetPropertyHeight(propertySettings) + m_padding * 2F;
        }
    }
}
