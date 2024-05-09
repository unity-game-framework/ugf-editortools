using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public abstract class DropdownWindowContent : PopupWindowContent
    {
        public Rect DropdownPosition { get; }
        public float MinHeight { get; set; } = EditorGUIUtility.singleLineHeight;
        public float MaxHeight { get; set; } = 200F;

        private float m_height;

        protected DropdownWindowContent(Rect dropdownPosition)
        {
            DropdownPosition = dropdownPosition;
        }

        protected abstract void OnGUILayout();

        protected virtual float OnGetHeight()
        {
            return Mathf.Clamp(m_height, MinHeight, MaxHeight);
        }

        public override void OnGUI(Rect rect)
        {
            float spacing = EditorGUIUtility.standardVerticalSpacing;

            rect.xMin += spacing;
            rect.yMin += spacing;
            rect.xMax -= spacing;
            rect.yMax -= spacing;

            GUILayout.BeginArea(rect);

            rect = EditorGUILayout.BeginVertical();

            OnGUILayout();

            EditorGUILayout.EndVertical();

            GUILayout.EndArea();

            if (Event.current.type == EventType.Repaint)
            {
                m_height = rect.height;
            }
        }

        public override Vector2 GetWindowSize()
        {
            return new Vector2(DropdownPosition.width, OnGetHeight() + EditorGUIUtility.standardVerticalSpacing * 2F);
        }
    }
}
