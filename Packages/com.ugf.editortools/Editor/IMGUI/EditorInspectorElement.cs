using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class EditorInspectorElement : VisualElement
    {
        public InspectorElement InspectorElement { get { return m_inspectorElement ?? throw new ArgumentException("Value not specified."); } }
        public bool HasInspectorElement { get { return m_inspectorElement != null; } }
        public bool DisplayTitlebar { get; set; } = true;

        private InspectorElement m_inspectorElement;

        public void SetTarget(Object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            if (HasInspectorElement)
            {
                ClearTarget();
            }

            m_inspectorElement = new InspectorElement(target);

            if (DisplayTitlebar)
            {
                Add(new EditorInspectorTitlebarElement(target));
            }

            Add(InspectorElement);
        }

        public void ClearTarget()
        {
            if (HasInspectorElement)
            {
                Remove(InspectorElement);
                Clear();

                m_inspectorElement = null;
            }
        }
    }
}
