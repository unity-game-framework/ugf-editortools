using System;
using System.IO;

namespace UGF.EditorTools.Editor
{
    public class EditorPathUtility
    {
        public static string GetAdditionalFilePath(string path, string label, string extensionName)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            if (string.IsNullOrEmpty(label)) throw new ArgumentException("Value cannot be null or empty.", nameof(label));
            if (string.IsNullOrEmpty(extensionName)) throw new ArgumentException("Value cannot be null or empty.", nameof(extensionName));

            string directory = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);

            return $"{directory}/{name}.{label}.{extensionName}";
        }
    }
}
