using System;
using UGF.EditorTools.Editor.Ids;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class EditorObjectReferenceIdDrawer : DrawerBase
    {
        public SerializedProperty SerializedProperty { get; }
        public EditorDrawer Drawer { get; } = new EditorDrawer();

        public EditorObjectReferenceIdDrawer(SerializedProperty serializedProperty)
        {
            SerializedProperty = serializedProperty ?? throw new ArgumentNullException(nameof(serializedProperty));
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            Drawer.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            Drawer.Disable();
        }

        public void DrawGUILayout()
        {
            string guid = GlobalIdEditorUtility.GetGuidFromProperty(SerializedProperty);
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<Object>(path);

            if (asset != null)
            {
                Drawer.Set(asset);
            }
            else
            {
                Drawer.Clear();
            }

            Drawer.DrawGUILayout();
        }
    }
}
