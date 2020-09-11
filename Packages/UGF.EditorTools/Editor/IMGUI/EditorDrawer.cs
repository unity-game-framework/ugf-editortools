using System;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class EditorDrawer
    {
        public UnityEditor.Editor Editor { get { return m_editor != null ? m_editor : throw new ArgumentException("Has no editor."); } }
        public bool HasEditor { get { return m_editor != null; } }

        private UnityEditor.Editor m_editor;

        public void DrawGUILayout()
        {
            if (m_editor != null)
            {
                m_editor.OnInspectorGUI();
            }
        }

        public bool Set(Object target)
        {
            if (m_editor == null || m_editor.target != target)
            {
                Clear();

                if (target != null)
                {
                    m_editor = UnityEditor.Editor.CreateEditor(target);
                }

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
