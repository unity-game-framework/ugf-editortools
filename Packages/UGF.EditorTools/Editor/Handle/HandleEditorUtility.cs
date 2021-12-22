using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Handle
{
    public static class HandleEditorUtility
    {
        public static void DrawWireCapsule(Vector3 position, float radius, float height)
        {
            Handles.DrawWireArc(position + new Vector3(0F, height - radius, 0F), Vector3.right, -Vector3.forward, 180F, radius);
            Handles.DrawWireArc(position + new Vector3(0F, height - radius, 0F), Vector3.forward, Vector3.right, 180F, radius);
            Handles.DrawWireDisc(position + new Vector3(0F, height - radius, 0F), Vector3.up, radius);
            Handles.DrawWireArc(position - new Vector3(0F, height - radius, 0F), Vector3.right, -Vector3.forward, -180F, radius);
            Handles.DrawWireArc(position - new Vector3(0F, height - radius, 0F), Vector3.forward, Vector3.right, -180F, radius);
            Handles.DrawWireDisc(position - new Vector3(0F, height - radius, 0F), Vector3.up, radius);
            Handles.DrawLine(position + new Vector3(radius, height - radius, 0F), position + new Vector3(radius, -(height - radius), 0F));
            Handles.DrawLine(position + new Vector3(-radius, height - radius, 0F), position + new Vector3(-radius, -(height - radius), 0F));
            Handles.DrawLine(position + new Vector3(0F, height - radius, radius), position + new Vector3(0F, -(height - radius), radius));
            Handles.DrawLine(position + new Vector3(0F, height - radius, -radius), position + new Vector3(0F, -(height - radius), -radius));
        }
    }
}
