using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public struct EditorProgressbarScope : IDisposable
    {
        public string Title
        {
            get { return m_title; }
            set
            {
                m_title = value;

                if (DisplayOnChange)
                {
                    Display();
                }
            }
        }

        public string Info
        {
            get { return m_info; }
            set
            {
                m_info = value;

                if (DisplayOnChange)
                {
                    Display();
                }
            }
        }

        public float Progress
        {
            get { return m_progress; }
            set
            {
                m_progress = value;

                if (DisplayOnChange)
                {
                    Display();
                }
            }
        }

        public bool DisplayOnChange { get; set; }

        private string m_title;
        private string m_info;
        private float m_progress;

        public EditorProgressbarScope(string title, string info, float progress = 0F, bool displayOnChange = true)
        {
            m_title = title;
            m_info = info;
            m_progress = progress;

            DisplayOnChange = displayOnChange;

            Display();
        }

        public void Display()
        {
            EditorUtility.DisplayProgressBar(m_title, m_info, m_progress);
        }

        public bool DisplayCancelable()
        {
            return EditorUtility.DisplayCancelableProgressBar(m_title, m_info, m_progress);
        }

        public void Dispose()
        {
            EditorUtility.ClearProgressBar();
        }
    }
}
