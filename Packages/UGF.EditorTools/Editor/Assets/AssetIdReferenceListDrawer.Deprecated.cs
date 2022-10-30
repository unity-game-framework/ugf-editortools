using System;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Assets
{
    public partial class AssetIdReferenceListDrawer
    {
        [Obsolete("DrawReplaceControlsLayout has been deprecated. Use 'DisplayReplaceButton' property to control whether to display replace controls or not.")]
        public void DrawReplaceControlsLayout()
        {
            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                DrawReplaceButtonLayout();
            }
        }

        [Obsolete("DrawReplaceButtonLayout has been deprecated. Use 'DisplayReplaceButton' property to control whether to display replace controls or not.")]
        public void DrawReplaceButtonLayout()
        {
            DisplayAsReplace = GUILayout.Toggle(DisplayAsReplace, "Replace", EditorStyles.miniButton);
        }
    }
}
