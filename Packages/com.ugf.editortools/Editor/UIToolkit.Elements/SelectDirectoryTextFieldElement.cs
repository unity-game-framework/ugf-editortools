using System;
using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.UIToolkit.SerializedProperties;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class SelectDirectoryTextFieldElement : TextField
    {
        public SerializedPropertyFieldBinding<string> PropertyBinding { get; }
        public IconButtonElement SelectButtonElement { get; }
        public string Title { get; set; }
        public string DefaultDirectory { get; set; }
        public bool Relative { get; set; }

        public SelectDirectoryTextFieldElement(SerializedProperty serializedProperty, bool field) : this()
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            if (field)
            {
                UIToolkitEditorUtility.AddFieldClasses(this);
            }

            PropertyBinding.Bind(serializedProperty);

            this.TrackPropertyValue(serializedProperty);
        }

        public SelectDirectoryTextFieldElement()
        {
            PropertyBinding = new SerializedPropertyFieldBinding<string>(this);
            PropertyBinding.Update += Update;
            PropertyBinding.Apply += Apply;

            SelectButtonElement = new IconButtonElement
            {
                iconImage = Background.FromTexture2D(EditorGUIUtility.FindTexture("FolderOpened Icon")),
                style =
                {
                    marginLeft = EditorGUIUtility.standardVerticalSpacing
                }
            };

            SelectButtonElement.clicked += OnSelectButtonClicked;

            Add(SelectButtonElement);
        }

        public void Update(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            value = serializedProperty.stringValue;
        }

        public void Apply(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            serializedProperty.stringValue = value;
            serializedProperty.serializedObject.ApplyModifiedProperties();
        }

        private void OnSelectButtonClicked()
        {
            string defaultDirectory = DefaultDirectory;

            if (!string.IsNullOrEmpty(value))
            {
                defaultDirectory = value;
            }

            if (AssetsEditorUtility.TrySelectDirectory(Title, defaultDirectory, Relative, out string path))
            {
                value = path;
            }
        }
    }
}
