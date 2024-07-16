using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class DropAreaElement : HelpBox
    {
        public DropAreaManipulator Manipulator { get; }

        public static string UssClassName { get; } = "ugf-drop-area";

        public DropAreaElement(Type acceptType)
        {
            Manipulator = new DropAreaManipulator(this, acceptType);

            text = "Drag and drop assets here.";

            Insert(0, new Image
            {
                image = AssetPreview.GetMiniTypeThumbnail(acceptType)
            });

            AddToClassList(UssClassName);

            UIToolkitEditorUtility.AddStyleSheets(this);
        }
    }
}
