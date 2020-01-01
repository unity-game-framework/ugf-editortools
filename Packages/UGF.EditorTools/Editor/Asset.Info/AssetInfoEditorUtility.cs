using System;
using System.IO;
using UnityEditor;

namespace UGF.EditorTools.Editor.Asset.Info
{
    public static class AssetInfoEditorUtility
    {
        public static T LoadInfo<T>(string path) where T : class, new()
        {
            return (T)LoadInfo(path, typeof(T));
        }

        public static object LoadInfo(string path, Type infoType)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            if (infoType == null) throw new ArgumentNullException(nameof(infoType));

            string text = File.ReadAllText(path);
            object info = Activator.CreateInstance(infoType);

            EditorJsonUtility.FromJsonOverwrite(text, info);

            return info;
        }

        public static void SaveInfo(string path, object info, bool import = true)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            if (info == null) throw new ArgumentNullException(nameof(info));

            string text = EditorJsonUtility.ToJson(info, true);

            File.WriteAllText(path, text);

            if (import)
            {
                AssetDatabase.ImportAsset(path);
            }
        }
    }
}
