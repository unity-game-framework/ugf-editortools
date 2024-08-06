using System;

namespace UGF.EditorTools.Editor.IMGUI.Toolbar
{
    public readonly struct ToolbarStyles
    {
        public string TabSingle { get; }
        public string TabFirst { get; }
        public string TabMiddle { get; }
        public string TabLast { get; }

        public ToolbarStyles(string tabSingle, string tabFirst, string tabMiddle, string tabLast)
        {
            TabSingle = tabSingle ?? throw new ArgumentNullException(nameof(tabSingle));
            TabFirst = tabFirst ?? throw new ArgumentNullException(nameof(tabFirst));
            TabMiddle = tabMiddle ?? throw new ArgumentNullException(nameof(tabMiddle));
            TabLast = tabLast ?? throw new ArgumentNullException(nameof(tabLast));
        }
    }
}
