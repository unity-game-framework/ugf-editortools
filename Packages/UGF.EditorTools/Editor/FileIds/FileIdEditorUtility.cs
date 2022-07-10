using System;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.FileIds
{
    public static class FileIdEditorUtility
    {
        public static ulong GetFileId(Object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            var id = GlobalObjectId.GetGlobalObjectIdSlow(target);

            return id.targetObjectId > 0 ? id.targetObjectId : id.targetPrefabId;
        }
    }
}
