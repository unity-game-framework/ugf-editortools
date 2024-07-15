using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class DropAreaElement : HelpBox
    {
        public DropAreaManipulator Manipulator { get; }

        public DropAreaElement(Type acceptType)
        {
            Manipulator = new DropAreaManipulator(this, acceptType);

            text = "Drag and drop assets here.";
            style.height = EditorGUIUtility.singleLineHeight * 3F;
            style.justifyContent = Justify.Center;

            Insert(0, new Image
            {
                image = AssetPreview.GetMiniTypeThumbnail(acceptType),
                style =
                {
                    width = EditorGUIUtility.singleLineHeight,
                    height = EditorGUIUtility.singleLineHeight,
                    marginRight = EditorGUIUtility.standardVerticalSpacing
                }
            });
        }
    }
}
