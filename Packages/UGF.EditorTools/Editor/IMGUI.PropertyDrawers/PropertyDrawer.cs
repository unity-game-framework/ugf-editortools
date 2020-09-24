using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.PropertyDrawers
{
    public abstract class PropertyDrawer<TAttribute> : PropertyDrawerBase where TAttribute : PropertyAttribute
    {
        public TAttribute Attribute { get { return (TAttribute)attribute; } }
    }
}
