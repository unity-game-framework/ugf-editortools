﻿using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public abstract class DropdownWindowContent : PopupWindowContent
    {
        public Rect DropdownPosition { get; }
        public float Padding { get; set; } = EditorGUIUtility.standardVerticalSpacing * 3F;
        public float MinWidth { get; set; } = 200F;
        public float MaxWidth { get; set; } = float.MaxValue;
        public float MinHeight { get; set; } = EditorGUIUtility.singleLineHeight;
        public float MaxHeight { get; set; } = 200F;

        private float m_height;

        protected DropdownWindowContent(Rect dropdownPosition)
        {
            DropdownPosition = dropdownPosition;
        }

        protected abstract void OnGUILayout();

        protected virtual float OnGetWidth()
        {
            return Mathf.Clamp(DropdownPosition.width, MinWidth, MaxWidth);
        }

        protected virtual float OnGetHeight()
        {
            return Mathf.Clamp(m_height, MinHeight, MaxHeight);
        }

        public override void OnGUI(Rect rect)
        {
            rect.xMin += Padding;
            rect.yMin += Padding;
            rect.xMax -= Padding;
            rect.yMax -= Padding;

            GUILayout.BeginArea(rect);

            rect = EditorGUILayout.BeginVertical();

            using (new LabelWidthScope(rect.width * 0.4F))
            {
                OnGUILayout();
            }

            EditorGUILayout.EndVertical();

            GUILayout.EndArea();

            if (Event.current.type == EventType.Repaint)
            {
                m_height = rect.height;
            }
        }

        public override Vector2 GetWindowSize()
        {
            return new Vector2(OnGetWidth(), OnGetHeight() + Padding * 2F);
        }
    }
}
