using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.PopupMenus
{
    public class PopupMenuContent : PopupWindowContent
    {
        public List<PopupMenuItem> Items { get; } = new List<PopupMenuItem>();

        public override void OnGUI(Rect rect)
        {
        }
    }
}
