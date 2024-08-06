using System;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace UGF.EditorTools.Editor.Tools
{
    public class ToolComponentBoundsHandleBox : ToolComponentBoundsHandle<BoxBoundsHandle>
    {
        public string PropertyCenterName { get; }
        public string PropertySizeName { get; }

        protected SerializedProperty PropertyCenter { get { return m_propertyCenter ?? throw new ArgumentException("Value not specified."); } }
        protected SerializedProperty PropertySize { get { return m_propertySize ?? throw new ArgumentException("Value not specified."); } }

        private SerializedProperty m_propertyCenter;
        private SerializedProperty m_propertySize;

        public ToolComponentBoundsHandleBox(string propertyCenterName, string propertySizeName) : base(new BoxBoundsHandle())
        {
            if (string.IsNullOrEmpty(propertyCenterName)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyCenterName));
            if (string.IsNullOrEmpty(propertySizeName)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertySizeName));

            PropertyCenterName = propertyCenterName;
            PropertySizeName = propertySizeName;
        }

        public override void OnActivated()
        {
            base.OnActivated();

            m_propertyCenter = SerializedObject.FindProperty(PropertyCenterName);
            m_propertySize = SerializedObject.FindProperty(PropertySizeName);
        }

        public override void OnWillBeDeactivated()
        {
            base.OnWillBeDeactivated();

            m_propertyCenter = null;
            m_propertySize = null;
        }

        protected override void OnHandleSetup()
        {
            base.OnHandleSetup();

            Handle.center = PropertyCenter.vector3Value;
            Handle.size = PropertySize.vector3Value;
        }

        protected override void OnHandleChanged()
        {
            base.OnHandleChanged();

            PropertyCenter.vector3Value = Handle.center;
            PropertySize.vector3Value = Handle.size;
        }
    }
}
