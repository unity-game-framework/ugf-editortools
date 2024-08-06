using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit
{
    public static class UIToolkitEditorUtility
    {
        public static string DataDirectoryPath { get; } = "Packages/com.ugf.editortools/Editor.Data/UIToolkit";
        public static string StyleSheetPath { get; } = $"{DataDirectoryPath}/UGF.EditorTools.Editor.Styles.uss";
        public static string StyleSheetThemeLightPath { get; } = $"{DataDirectoryPath}/UGF.EditorTools.Editor.Styles.Light.tss";
        public static string StyleSheetThemeDarkPath { get; } = $"{DataDirectoryPath}/UGF.EditorTools.Editor.Styles.Dark.tss";
        public static StyleSheet StyleSheet { get; }
        public static ThemeStyleSheet StyleSheetThemeLight { get; }
        public static ThemeStyleSheet StyleSheetThemeDark { get; }
        public static ThemeStyleSheet CurrentStyleSheetTheme { get { return EditorGUIUtility.isProSkin ? StyleSheetThemeDark : StyleSheetThemeLight; } }

        static UIToolkitEditorUtility()
        {
            StyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(StyleSheetPath) ?? throw new ArgumentException("UIToolkit Styles not found.");
            StyleSheetThemeLight = AssetDatabase.LoadAssetAtPath<ThemeStyleSheet>(StyleSheetThemeLightPath) ?? throw new ArgumentException("UIToolkit Styles not found.");
            StyleSheetThemeDark = AssetDatabase.LoadAssetAtPath<ThemeStyleSheet>(StyleSheetThemeDarkPath) ?? throw new ArgumentException("UIToolkit Styles not found.");
        }

        public static void AddStyleSheets(VisualElement visualElement)
        {
            if (visualElement == null) throw new ArgumentNullException(nameof(visualElement));

            visualElement.styleSheets.Add(StyleSheet);
            visualElement.styleSheets.Add(CurrentStyleSheetTheme);
        }

        public static void AddFieldClasses<T>(BaseField<T> field)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));

            field.AddToClassList(BaseField<T>.alignedFieldUssClassName);
        }

        public static void AddObjectFieldContextMenuActions(ObjectField field, ContextualMenuPopulateEvent populateEvent)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));
            if (populateEvent == null) throw new ArgumentNullException(nameof(populateEvent));

            populateEvent.menu.InsertAction(
                0,
                "Copy GUID",
                action =>
                {
                    var objectField = (ObjectField)action.userData;
                    string path = AssetDatabase.GetAssetPath(objectField.value);
                    string guid = AssetDatabase.AssetPathToGUID(path);

                    GUIUtility.systemCopyBuffer = guid;
                },
                action =>
                {
                    var objectField = (ObjectField)action.userData;
                    string path = AssetDatabase.GetAssetPath(objectField.value);
                    string guid = AssetDatabase.AssetPathToGUID(path);

                    return !string.IsNullOrEmpty(guid) ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
                },
                field
            );

            populateEvent.menu.InsertAction(
                0,
                "Copy Path",
                action =>
                {
                    var objectField = (ObjectField)action.userData;
                    string path = AssetDatabase.GetAssetPath(objectField.value);

                    GUIUtility.systemCopyBuffer = path;
                },
                action =>
                {
                    var objectField = (ObjectField)action.userData;
                    string path = AssetDatabase.GetAssetPath(objectField.value);

                    return !string.IsNullOrEmpty(path) ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
                },
                field
            );
        }
    }
}
