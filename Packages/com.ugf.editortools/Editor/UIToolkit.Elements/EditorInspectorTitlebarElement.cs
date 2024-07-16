using System;
using UnityEditor;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class EditorInspectorTitlebarElement : IMGUIContainer
    {
        public Object Target { get; }

        public EditorInspectorTitlebarElement(Object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            Target = target;

            onGUIHandler = OnGUI;
        }

        private void OnGUI()
        {
            EditorGUILayout.InspectorTitlebar(true, Target, false);
        }
    }
}
