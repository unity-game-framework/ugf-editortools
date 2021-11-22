using System;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace UGF.EditorTools.Editor.Tools
{
    public class ToolComponentBoundsHandleSphere : ToolComponentBoundsHandle<SphereBoundsHandle>
    {
        public string PropertyCenterName { get; }
        public string PropertyRadiusName { get; }

        public SerializedProperty PropertyCenter { get { return m_propertyCenter ?? throw new ArgumentException("Value not specified."); } }
        public SerializedProperty PropertyRadius { get { return m_propertyRadius ?? throw new ArgumentException("Value not specified."); } }

        private SerializedProperty m_propertyCenter;
        private SerializedProperty m_propertyRadius;

        public ToolComponentBoundsHandleSphere(string propertyCenterName, string propertyRadiusName) : base(new SphereBoundsHandle())
        {
            if (string.IsNullOrEmpty(propertyCenterName)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyCenterName));
            if (string.IsNullOrEmpty(propertyRadiusName)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyRadiusName));

            PropertyCenterName = propertyCenterName;
            PropertyRadiusName = propertyRadiusName;
        }

        public override void OnActivated()
        {
            base.OnActivated();

            m_propertyCenter = SerializedObject.FindProperty(PropertyCenterName);
            m_propertyRadius = SerializedObject.FindProperty(PropertyRadiusName);
        }

        public override void OnWillBeDeactivated()
        {
            base.OnWillBeDeactivated();

            m_propertyCenter = null;
            m_propertyRadius = null;
        }

        protected override void OnHandleSetup()
        {
            base.OnHandleSetup();

            Handle.center = PropertyCenter.vector3Value;
            Handle.radius = PropertyRadius.floatValue;
        }

        protected override void OnHandleChanged()
        {
            base.OnHandleChanged();

            PropertyCenter.vector3Value = Handle.center;
            PropertyRadius.floatValue = Handle.radius;
        }
    }
}
