using System;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.SettingsGroups
{
    public class SettingsGroupsDrawer : DrawerBase
    {
        private Styles m_styles;

        private class Styles
        {
            public GUIStyle FrameBox { get; } = new GUIStyle("FrameBox");
            public GUIStyle TabOne { get; } = new GUIStyle("Tab onlyOne");
            public GUIStyle TabFirst { get; } = new GUIStyle("Tab first");
            public GUIStyle TabMiddle { get; } = new GUIStyle("Tab middle");
            public GUIStyle TabLast { get; } = new GUIStyle("Tab last");
        }

        public int DrawGUILayout(SerializedProperty propertyGroups, int selected)
        {
            if (propertyGroups == null) throw new ArgumentNullException(nameof(propertyGroups));
            if (selected < 0 || selected >= propertyGroups.arraySize) throw new ArgumentOutOfRangeException(nameof(selected));
            if (m_styles == null) m_styles = new Styles();

            return OnDrawGUILayout(propertyGroups, selected);
        }

        public int DrawGUI(Rect position, SerializedProperty propertyGroups, int selected)
        {
            if (propertyGroups == null) throw new ArgumentNullException(nameof(propertyGroups));
            if (selected < 0 || selected >= propertyGroups.arraySize) throw new ArgumentOutOfRangeException(nameof(selected));
            if (m_styles == null) m_styles = new Styles();

            return OnDrawGUI(position, propertyGroups, selected);
        }

        protected virtual int OnDrawGUILayout(SerializedProperty propertyGroups, int selected)
        {
            float height = OnGetHeight(propertyGroups, selected);
            Rect position = EditorGUILayout.GetControlRect(false, height);

            return OnDrawGUI(position, propertyGroups, selected);
        }

        protected virtual int OnDrawGUI(Rect position, SerializedProperty propertyGroups, int selected)
        {
            if (Event.current.type == EventType.Repaint)
            {
                m_styles.FrameBox.Draw(position, false, false, false, false);
            }

            float toolbarHeight = OnGetToolbarHeight(propertyGroups);
            var toolbarPosition = new Rect(position.x, position.y, position.width, toolbarHeight);

            float settingsHeight = OnGetSettingsHeight(propertyGroups, selected);
            var settingsPosition = new Rect(position.x, toolbarPosition.yMax, position.width, settingsHeight);

            selected = OnDrawToolbar(toolbarPosition, propertyGroups, selected);

            OnDrawSettings(settingsPosition, propertyGroups, selected);

            return selected;
        }

        protected virtual int OnDrawToolbar(Rect position, SerializedProperty propertyGroups, int selected)
        {
            int count = propertyGroups.arraySize;
            float tabWidth = position.width / count;

            for (int i = 0; i < propertyGroups.arraySize; i++)
            {
                var tabPosition = new Rect(position.x + tabWidth * i, position.y, tabWidth, position.height);

                if (OnDrawToolbarTab(tabPosition, propertyGroups, i, selected))
                {
                    selected = i;
                }
            }

            return selected;
        }

        protected virtual bool OnDrawToolbarTab(Rect position, SerializedProperty propertyGroups, int index, int selected)
        {
            GUIContent label = OnGetGroupLabel(propertyGroups, index);
            GUIStyle style = GetTabStyle(propertyGroups, index);
            bool state = index == selected;

            return GUI.Toggle(position, state, label, style) != state;
        }

        protected virtual void OnDrawSettings(Rect position, SerializedProperty propertyGroups, int index)
        {
            float padding = 5F;

            position.xMin += padding;
            position.xMax -= padding;
            position.yMin += padding;
            position.yMax -= padding;

            SerializedProperty propertyGroup = propertyGroups.GetArrayElementAtIndex(index);
            SerializedProperty propertySettings = propertyGroup.FindPropertyRelative("m_settings");
            GUIContent label = OnGetGroupLabel(propertyGroups, index);

            using (new IndentIncrementScope(1))
            using (new LabelWidthChangeScope(-padding))
            {
                EditorGUI.PropertyField(position, propertySettings, label, true);
            }
        }

        protected virtual float OnGetHeight(SerializedProperty propertyGroups, int selected)
        {
            float toolbar = OnGetToolbarHeight(propertyGroups);
            float settings = OnGetSettingsHeight(propertyGroups, selected);

            return toolbar + settings;
        }

        protected virtual float OnGetToolbarHeight(SerializedProperty propertyGroups)
        {
            return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 3F;
        }

        protected virtual float OnGetSettingsHeight(SerializedProperty propertyGroups, int index)
        {
            SerializedProperty propertyGroup = propertyGroups.GetArrayElementAtIndex(index);
            SerializedProperty propertySettings = propertyGroup.FindPropertyRelative("m_settings");

            float padding = 5F;

            return EditorGUI.GetPropertyHeight(propertySettings) + padding * 2F;
        }

        protected virtual GUIContent OnGetGroupLabel(SerializedProperty propertyGroups, int index)
        {
            SerializedProperty propertyGroup = propertyGroups.GetArrayElementAtIndex(index);
            SerializedProperty propertyName = propertyGroup.FindPropertyRelative("m_name");

            return new GUIContent(propertyName.stringValue);
        }

        private GUIStyle GetTabStyle(SerializedProperty propertyGroups, int index)
        {
            int count = propertyGroups.arraySize;

            if (count == 1)
            {
                return m_styles.TabOne;
            }

            if (index == 0)
            {
                return m_styles.TabFirst;
            }

            if (index == count - 1)
            {
                return m_styles.TabLast;
            }

            return m_styles.TabMiddle;
        }
    }
}
