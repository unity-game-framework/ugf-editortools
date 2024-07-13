using System;
using UGF.EditorTools.Editor.Serialized;
using UGF.EditorTools.Editor.UIToolkit;
using UGF.EditorTools.Editor.UIToolkit.SerializedProperties;
using UnityEditor;
using UnityEditor.UIElements;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.IMGUI.AssetReferences
{
    public class AssetReferenceObjectFieldElement : ObjectField
    {
        public SerializedPropertyFieldBinding<Object> PropertyBinding { get; }

        public AssetReferenceObjectFieldElement(SerializedProperty serializedProperty, bool field = false) : this()
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            if (field)
            {
                UIToolkitEditorUtility.AddFieldClasses(this);
            }

            SerializedProperty propertyAsset = serializedProperty.FindPropertyRelative("m_asset");

            PropertyBinding.Bind(serializedProperty);

            bindingPath = propertyAsset.propertyPath;
            objectType = SerializedPropertyEditorUtility.GetFieldType(propertyAsset);

            this.TrackPropertyValue(serializedProperty);
        }

        public AssetReferenceObjectFieldElement()
        {
            PropertyBinding = new SerializedPropertyFieldBinding<Object>(this);
            PropertyBinding.Update += Update;
            PropertyBinding.Apply += Apply;
        }

        public void Update(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            if (!serializedProperty.hasMultipleDifferentValues)
            {
                SerializedProperty propertyAsset = serializedProperty.FindPropertyRelative("m_asset");

                value = propertyAsset.objectReferenceValue;
            }
        }

        public void Apply(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            SerializedProperty propertyGuid = serializedProperty.FindPropertyRelative("m_guid");
            SerializedProperty propertyAsset = serializedProperty.FindPropertyRelative("m_asset");

            string path = AssetDatabase.GetAssetPath(value);
            string guid = AssetDatabase.AssetPathToGUID(path);

            propertyGuid.stringValue = guid;
            propertyAsset.objectReferenceValue = value;

            serializedProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}
