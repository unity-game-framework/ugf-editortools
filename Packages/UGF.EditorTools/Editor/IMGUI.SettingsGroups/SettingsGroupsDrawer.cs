using System;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.SettingsGroups
{
    public class SettingsGroupsDrawer : DrawerBase
    {
        public SettingsGroupsToolbarDrawer Toolbar { get; set; } = new SettingsGroupsToolbarDrawer();

        private Styles m_styles;

        private class Styles
        {
            public GUIStyle FrameBox { get; } = new GUIStyle("FrameBox");
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

        protected virtual void OnDrawGUI(Rect position, SerializedProperty propertyGroups)
        {
            if (Event.current.type == EventType.Repaint)
            {
                m_styles.FrameBox.Draw(position, false, false, false, false);
            }

            float toolbarHeight = OnGetToolbarHeight();
            var toolbarPosition = new Rect(position.x, position.y, position.width, toolbarHeight);

            float settingsHeight = OnGetSettingsHeight(propertyGroups, Toolbar.Selected);
            var settingsPosition = new Rect(position.x, toolbarPosition.yMax, position.width, settingsHeight);

            OnDrawToolbar(toolbarPosition);
            OnDrawSettings(settingsPosition, propertyGroups, Toolbar.Selected);
        }

        protected virtual void OnDrawToolbar(Rect position)
        {
            Toolbar.DrawGUI(position);
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

            using (new IndentIncrementScope(1))
            using (new LabelWidthChangeScope(-padding))
            {
                EditorGUI.PropertyField(position, propertySettings, true);
            }
        }

        protected virtual float OnGetHeight(SerializedProperty propertyGroups)
        {
            float toolbar = OnGetToolbarHeight();
            float settings = OnGetSettingsHeight(propertyGroups, Toolbar.Selected);

            return toolbar + settings;
        }

        protected virtual float OnGetToolbarHeight()
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
    }
}
