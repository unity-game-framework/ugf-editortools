using System;
using System.IO;
using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.UIToolkit.SerializedProperties;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class SelectFileTextFieldElement : TextField
    {
        public SerializedPropertyFieldBinding<string> PropertyBinding { get; }
        public IconButtonElement SelectButtonElement { get; }
        public string Title { get; set; }
        public string DefaultDirectory { get; set; }
        public string Extension { get; set; }
        public bool InAssets { get; set; }

        public SelectFileTextFieldElement(SerializedProperty serializedProperty, bool field) : this()
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            if (field)
            {
                UIToolkitEditorUtility.AddFieldClasses(this);
            }

            PropertyBinding.Bind(serializedProperty);

            this.TrackPropertyValue(serializedProperty);
        }

        public SelectFileTextFieldElement()
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
                string fileDirectory = Path.GetDirectoryName(value);

                if (!string.IsNullOrEmpty(fileDirectory))
                {
                    defaultDirectory = fileDirectory;
                }
            }

            if (AssetsEditorUtility.TrySelectFile(Title, defaultDirectory, Extension, InAssets, out string path))
            {
                value = path;
            }
        }
    }
}
