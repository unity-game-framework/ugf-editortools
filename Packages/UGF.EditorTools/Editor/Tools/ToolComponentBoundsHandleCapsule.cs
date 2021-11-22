using System;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace UGF.EditorTools.Editor.Tools
{
    public class ToolComponentBoundsHandleCapsule : ToolComponentBoundsHandle<CapsuleBoundsHandle>
    {
        public string PropertyCenterName { get; }
        public string PropertyHeightName { get; }
        public string PropertyRadiusName { get; }

        public SerializedProperty PropertyCenter { get { return m_propertyCenter ?? throw new ArgumentException("Value not specified."); } }
        public SerializedProperty PropertyHeight { get { return m_propertyHeight ?? throw new ArgumentException("Value not specified."); } }
        public SerializedProperty PropertyRadius { get { return m_propertyRadius ?? throw new ArgumentException("Value not specified."); } }

        private SerializedProperty m_propertyCenter;
        private SerializedProperty m_propertyHeight;
        private SerializedProperty m_propertyRadius;

        public ToolComponentBoundsHandleCapsule(string propertyCenterName, string propertyHeightName, string propertyRadiusName) : base(new CapsuleBoundsHandle())
        {
            if (string.IsNullOrEmpty(propertyCenterName)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyCenterName));
            if (string.IsNullOrEmpty(propertyHeightName)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyHeightName));
            if (string.IsNullOrEmpty(propertyRadiusName)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyRadiusName));

            PropertyCenterName = propertyCenterName;
            PropertyHeightName = propertyHeightName;
            PropertyRadiusName = propertyRadiusName;
        }

        public override void OnActivated()
        {
            base.OnActivated();

            m_propertyCenter = SerializedObject.FindProperty(PropertyCenterName);
            m_propertyHeight = SerializedObject.FindProperty(PropertyHeightName);
            m_propertyRadius = SerializedObject.FindProperty(PropertyRadiusName);
        }

        public override void OnWillBeDeactivated()
        {
            base.OnWillBeDeactivated();

            m_propertyCenter = null;
            m_propertyHeight = null;
            m_propertyRadius = null;
        }

        protected override void OnHandleSetup()
        {
            base.OnHandleSetup();

            Handle.center = PropertyCenter.vector3Value;
            Handle.height = PropertyHeight.floatValue;
            Handle.radius = PropertyRadius.floatValue;
        }

        protected override void OnHandleChanged()
        {
            base.OnHandleChanged();

            PropertyCenter.vector3Value = Handle.center;
            PropertyHeight.floatValue = Handle.height;
            PropertyRadius.floatValue = Handle.radius;
        }
    }
}
