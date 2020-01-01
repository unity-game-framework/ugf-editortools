using System;
using System.IO;

namespace UGF.EditorTools.Editor.Asset
{
    public class AssetEditorUtility
    {
        public static string GetAdditionalFilePath(string assetPath, string label, string extensionName)
        {
            if (string.IsNullOrEmpty(assetPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(assetPath));
            if (string.IsNullOrEmpty(label)) throw new ArgumentException("Value cannot be null or empty.", nameof(label));
            if (string.IsNullOrEmpty(extensionName)) throw new ArgumentException("Value cannot be null or empty.", nameof(extensionName));

            string directory = Path.GetDirectoryName(assetPath);
            string name = Path.GetFileNameWithoutExtension(assetPath);

            return $"{directory}/{name}.{label}.{extensionName}";
        }
    }
}
