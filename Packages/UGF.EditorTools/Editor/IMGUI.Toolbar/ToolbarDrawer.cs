using System;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Toolbar
{
    public class ToolbarDrawer : DrawerBase
    {
        public int Count { get; set; }
        public int Selected { get; set; }
        public ToolbarDirection Direction { get; set; } = ToolbarDirection.Horizontal;
        public ToolbarStyles Styles { get; set; } = new ToolbarStyles("miniButton", "miniButtonLeft", "miniButtonMid", "miniButtonRight");

        private StylesCache m_styles;

        private class StylesCache
        {
            public GUIStyle TabSingle { get; }
            public GUIStyle TabFirst { get; }
            public GUIStyle TabMiddle { get; }
            public GUIStyle TabLast { get; }

            public StylesCache(ToolbarStyles styles)
            {
                TabSingle = new GUIStyle(styles.TabSingle);
                TabFirst = new GUIStyle(styles.TabMiddle);
                TabMiddle = new GUIStyle(styles.TabMiddle);
                TabLast = new GUIStyle(styles.TabLast);
            }
        }

        public void DrawGUILayout()
        {
            float line = EditorGUIUtility.singleLineHeight;
            float height = Direction == ToolbarDirection.Horizontal ? line : line * Count;
            Rect position = EditorGUILayout.GetControlRect(false, height);

            DrawGUI(position);
        }

        public void DrawGUI(Rect position)
        {
            if (m_styles == null)
            {
                m_styles = new StylesCache(Styles);
            }

            OnDrawGUI(position);
        }

        protected virtual void OnDrawGUI(Rect position)
        {
            switch (Direction)
            {
                case ToolbarDirection.Horizontal:
                {
                    OnDrawHorizontal(position);
                    break;
                }
                case ToolbarDirection.Vertical:
                {
                    OnDrawVertical(position);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual void OnDrawHorizontal(Rect position)
        {
            float tabWidth = position.width / Count;

            for (int i = 0; i < Count; i++)
            {
                var tabPosition = new Rect(position.x + tabWidth * i, position.y, tabWidth, position.height);

                if (OnDrawTab(tabPosition, i))
                {
                    Selected = i;
                }
            }
        }

        protected virtual void OnDrawVertical(Rect position)
        {
            float tabHeight = position.height / Count;

            for (int i = 0; i < Count; i++)
            {
                var tabPosition = new Rect(position.x, position.y + tabHeight * i, position.width, tabHeight);

                if (OnDrawTab(tabPosition, i))
                {
                    Selected = i;
                }
            }
        }

        protected virtual bool OnDrawTab(Rect position, int index)
        {
            GUIContent label = OnGetTabLabel(index);
            GUIStyle style = OnGetTabStyle(index);
            bool state = index == Selected;

            return GUI.Toggle(position, state, label, style) != state;
        }

        protected virtual GUIContent OnGetTabLabel(int index)
        {
            return new GUIContent($"Tab {index}");
        }

        protected virtual GUIStyle OnGetTabStyle(int index)
        {
            int count = Count;

            if (count == 1)
            {
                return m_styles.TabSingle;
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
