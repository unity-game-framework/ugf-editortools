using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit
{
    public static class UIToolkitEditorUtility
    {
        public static StyleSheet StyleSheet { get; }
        public static ThemeStyleSheet StyleSheetLight { get; }
        public static ThemeStyleSheet StyleSheetDark { get; }

        static UIToolkitEditorUtility()
        {
            StyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Packages/com.ugf.editortools/Data.Editor/UGF.EditorTools.Editor.Styles.uss") ?? throw new ArgumentException("UIToolkit Styles not found.");
            StyleSheetLight = AssetDatabase.LoadAssetAtPath<ThemeStyleSheet>("Packages/com.ugf.editortools/Data.Editor/UGF.EditorTools.Editor.Styles.Light.tss") ?? throw new ArgumentException("UIToolkit Styles not found.");
            StyleSheetDark = AssetDatabase.LoadAssetAtPath<ThemeStyleSheet>("Packages/com.ugf.editortools/Data.Editor/UGF.EditorTools.Editor.Styles.Dark.tss") ?? throw new ArgumentException("UIToolkit Styles not found.");
        }

        public static void AddFieldClasses<T>(BaseField<T> field)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));

            field.AddToClassList(BaseField<T>.alignedFieldUssClassName);
        }
    }
}
