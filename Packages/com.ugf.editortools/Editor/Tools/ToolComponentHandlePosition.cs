using System;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tools
{
    public class ToolComponentHandlePosition : ToolComponentHandle
    {
        public string PropertyPositionName { get; }
        public override GUIContent toolbarIcon { get { return ToolEditorUtility.EditPositionContent; } }

        protected SerializedProperty PropertyPosition { get { return m_propertyPosition ?? throw new ArgumentException("Value not specified."); } }

        private SerializedProperty m_propertyPosition;
        private Vector3 m_position;

        public ToolComponentHandlePosition(string propertyPositionName)
        {
            if (string.IsNullOrEmpty(propertyPositionName)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyPositionName));

            PropertyPositionName = propertyPositionName;
        }

        public override void OnActivated()
        {
            base.OnActivated();

            m_propertyPosition = SerializedObject.FindProperty(PropertyPositionName);
        }

        public override void OnWillBeDeactivated()
        {
            base.OnWillBeDeactivated();

            m_propertyPosition = null;
        }

        protected override void OnHandleSetup()
        {
            base.OnHandleSetup();

            m_position = PropertyPosition.vector3Value;
        }

        protected override void OnHandleChanged()
        {
            base.OnHandleChanged();

            PropertyPosition.vector3Value = m_position;
        }

        protected override void OnHandleDraw()
        {
            base.OnHandleDraw();

            Matrix4x4 matrix = OnGetMatrix();
            Vector3 position = matrix.MultiplyPoint3x4(m_position);
            Quaternion rotation = UnityEditor.Tools.pivotRotation == PivotRotation.Local ? Component.transform.rotation : Quaternion.identity;

            using (new Handles.DrawingScope(Matrix4x4.identity))
            {
                position = Handles.PositionHandle(position, rotation);
            }

            m_position = matrix.inverse.MultiplyPoint3x4(position);
        }
    }
}
