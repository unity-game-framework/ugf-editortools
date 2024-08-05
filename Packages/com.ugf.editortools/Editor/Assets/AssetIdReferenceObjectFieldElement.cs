using System;
using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Editor.Serialized;
using UGF.EditorTools.Editor.UIToolkit;
using UGF.EditorTools.Editor.UIToolkit.SerializedProperties;
using UnityEditor;
using UnityEditor.UIElements;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.Assets
{
    public class AssetIdReferenceObjectFieldElement : ObjectField
    {
        public SerializedPropertyFieldBinding<Object> PropertyBinding { get; }

        public AssetIdReferenceObjectFieldElement(SerializedProperty serializedProperty, bool field = false) : this()
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            if (field)
            {
                UIToolkitEditorUtility.AddFieldClasses(this);
            }

            Bind(serializedProperty);
        }

        public AssetIdReferenceObjectFieldElement()
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

            propertyAsset.objectReferenceValue = value;

            GlobalIdEditorUtility.SetGuidToProperty(propertyGuid, guid);

            serializedProperty.serializedObject.ApplyModifiedProperties();
        }

        public void Bind(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            SerializedProperty propertyAsset = serializedProperty.FindPropertyRelative("m_asset");

            PropertyBinding.Bind(serializedProperty);

            bindingPath = propertyAsset.propertyPath;
            objectType = SerializedPropertyEditorUtility.GetFieldType(propertyAsset);

            this.TrackPropertyValue(serializedProperty);
        }

        public void Unbind(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            PropertyBinding.Unbind();

            bindingPath = string.Empty;
            objectType = typeof(Object);

            this.Unbind();
        }
    }
}
