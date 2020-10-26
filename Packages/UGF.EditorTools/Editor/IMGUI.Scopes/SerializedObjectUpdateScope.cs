using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public readonly struct SerializedObjectUpdateScope : IDisposable
    {
        private readonly SerializedObject m_serializedObject;
        private readonly bool m_applyWithoutUndo;

        public SerializedObjectUpdateScope(SerializedObject serializedObject, bool forceUpdate = false, bool applyWithoutUndo = false)
        {
            m_serializedObject = serializedObject ?? throw new ArgumentNullException(nameof(serializedObject));
            m_applyWithoutUndo = applyWithoutUndo;

            if (forceUpdate)
            {
                m_serializedObject.Update();
            }
            else
            {
                m_serializedObject.UpdateIfRequiredOrScript();
            }
        }

        public void Dispose()
        {
            if (m_applyWithoutUndo)
            {
                m_serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }
            else
            {
                m_serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
