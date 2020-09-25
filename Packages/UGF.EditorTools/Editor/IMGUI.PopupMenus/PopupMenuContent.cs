using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.PopupMenus
{
    public class PopupMenuContent : PopupWindowContent
    {
        public List<PopupMenuItem> Items { get; } = new List<PopupMenuItem>();
        public float ItemHeight { get; set; } = EditorGUIUtility.singleLineHeight;
        public float Spacing { get; set; } = EditorGUIUtility.standardVerticalSpacing;
        public Vector4 Padding { get; set; } = Vector4.one * EditorGUIUtility.standardVerticalSpacing;

        public override void OnGUI(Rect position)
        {
            OnDrawItems(position);
        }

        public override Vector2 GetWindowSize()
        {
            Vector2 size = base.GetWindowSize();

            size.x = 500F;
            size.y = OnGetWindowHeight();

            return size;
        }

        protected virtual void OnDrawItems(Rect position)
        {
            position.xMin += Padding.x;
            position.xMax -= Padding.y;
            position.yMin += Padding.z;
            position.yMax -= Padding.w;

            for (int i = 0; i < Items.Count; i++)
            {
                PopupMenuItem item = Items[i];
                float height = OnGetItemHeight(item);

                position.height = height;

                OnDrawItem(position, item);

                position.y += height + Spacing;
            }
        }

        protected virtual void OnDrawItem(Rect position, PopupMenuItem item)
        {
            GUI.Button(position, item.Content);
        }

        protected virtual float OnGetWindowHeight()
        {
            float height = Spacing;

            for (int i = 0; i < Items.Count; i++)
            {
                PopupMenuItem item = Items[i];

                height += OnGetItemHeight(item);
                height += Spacing;
            }

            return height;
        }

        protected virtual float OnGetItemHeight(PopupMenuItem item)
        {
            return ItemHeight;
        }
    }
}
