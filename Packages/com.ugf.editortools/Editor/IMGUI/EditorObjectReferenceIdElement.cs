using System;
using UGF.EditorTools.Editor.Ids;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class EditorObjectReferenceIdElement : EditorInspectorElement
    {
        public SerializedProperty SerializedProperty { get; }

        public EditorObjectReferenceIdElement(SerializedProperty serializedProperty)
        {
            SerializedProperty = serializedProperty ?? throw new ArgumentNullException(nameof(serializedProperty));

            RegisterCallback<AttachToPanelEvent>(OnAttach);

            this.TrackPropertyValue(serializedProperty, OnChange);
        }

        private void OnAttach(AttachToPanelEvent attachToPanelEvent)
        {
            OnUpdate();
        }

        private void OnChange(SerializedProperty serializedProperty)
        {
            OnUpdate();
        }

        private void OnUpdate()
        {
            string guid = GlobalIdEditorUtility.GetGuidFromProperty(SerializedProperty);
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<Object>(path);

            if (asset != null && asset != SerializedProperty.serializedObject.targetObject)
            {
                SetTarget(asset);
            }
            else
            {
                ClearTarget();
            }
        }
    }
}
