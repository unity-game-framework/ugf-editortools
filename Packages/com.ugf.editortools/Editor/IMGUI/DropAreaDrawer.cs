using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class DropAreaDrawer : DrawerBase
    {
        public DropAreaHandler Handler { get; }
        public string DisplayMessage { get; set; } = "Drag and drop assets here.";
        public float Height { get; set; } = EditorGUIUtility.singleLineHeight * 3F;

        private Styles m_styles;

        private class Styles
        {
            public GUIStyle BackgroundStyle { get; } = new GUIStyle(EditorStyles.helpBox)
            {
                alignment = TextAnchor.MiddleCenter
            };

            public GUIContent MessageContent { get; } = new GUIContent(EditorGUIUtility.FindTexture("Toolbar Plus"));
        }

        public DropAreaDrawer() : this(typeof(Object))
        {
        }

        public DropAreaDrawer(Type acceptType) : this(new DropAreaHandler(acceptType))
        {
        }

        public DropAreaDrawer(DropAreaHandler handler)
        {
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public void DrawGUILayout()
        {
            Rect position = EditorGUILayout.GetControlRect(false, Height);

            DrawGUI(position);
        }

        public void DrawGUI(Rect position)
        {
            m_styles ??= new Styles();
            m_styles.MessageContent.text = DisplayMessage;

            GUI.Label(position, m_styles.MessageContent, m_styles.BackgroundStyle);

            m_styles.MessageContent.text = string.Empty;

            Handler.Handle(position);
        }
    }
}
