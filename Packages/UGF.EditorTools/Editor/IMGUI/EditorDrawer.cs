using System;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class EditorDrawer
    {
        public UnityEditor.Editor Editor { get { return m_editor != null ? m_editor : throw new ArgumentException("Has no editor."); } }
        public bool HasEditor { get { return m_editor != null; } }
        public bool DisplayTitlebar { get; set; } = true;

        private UnityEditor.Editor m_editor;

        public void DrawGUILayout()
        {
            if (m_editor != null)
            {
                if (DisplayTitlebar)
                {
                    EditorGUILayout.InspectorTitlebar(true, m_editor, false);
                }

                m_editor.OnInspectorGUI();
            }
        }

        public bool Set(Object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            if (m_editor == null || m_editor.target != target)
            {
                Clear();

                m_editor = UnityEditor.Editor.CreateEditor(target);

                return true;
            }

            return false;
        }

        public void Clear()
        {
            if (m_editor != null)
            {
                Object.DestroyImmediate(m_editor);

                m_editor = null;
            }
        }
    }
}
