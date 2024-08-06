using System.IO;
using UGF.EditorTools.Editor.UIToolkit.Elements;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.Tests.UIToolkit
{
    [CreateAssetMenu(menuName = "Tests/TestStylesAsset")]
    public class TestStylesAsset : ScriptableObject
    {
        [ContextMenu("ExtractStyles")]
        public void ExtractStyles()
        {
            var skin = CreateInstance<GUISkin>();

            skin.customStyles = new[]
            {
                EditorStyles.iconButton
            };

            string path = AssetDatabase.GetAssetPath(this);
            string folder = Path.GetDirectoryName(path);

            AssetDatabase.CreateAsset(skin, $"{folder}/TestStylesSkin.guiskin");
        }
    }

    [CustomEditor(typeof(TestStylesAsset))]
    internal class TestStylesAssetEditor : UnityEditor.Editor
    {
        private Styles m_styles;

        private class Styles
        {
            public GUIContent ButtonContent { get; } = new GUIContent(EditorGUIUtility.FindTexture("_Menu"));
        }

        public override VisualElement CreateInspectorGUI()
        {
            var element = new VisualElement();

            element.Add(new IconButtonElement
            {
                iconImage = Background.FromTexture2D(EditorGUIUtility.FindTexture("_Menu"))
            });

            element.Add(new IconButtonElement
            {
                iconImage = Background.FromTexture2D(EditorGUIUtility.FindTexture("_Menu")),
                enabledSelf = false
            });

            element.Add(new IMGUIContainer(OnGUI));

            return element;
        }

        private void OnGUI()
        {
            m_styles ??= new Styles();

            GUILayout.Button(m_styles.ButtonContent, EditorStyles.iconButton);

            using (new EditorGUI.DisabledScope(true))
            {
                GUILayout.Button(m_styles.ButtonContent, EditorStyles.iconButton);
            }
        }
    }
}
