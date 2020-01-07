using System;
using System.IO;
using UnityEditor;

namespace UGF.EditorTools.Editor
{
    public class EditorTempScope : IDisposable
    {
        public string Path { get; }
        public bool IsDirectory { get; }

        public EditorTempScope(bool isDirectory = false) : this(FileUtil.GetUniqueTempPathInProject(), isDirectory)
        {
        }

        public EditorTempScope(string path, bool isDirectory = false)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            Path = path;
            IsDirectory = isDirectory;

            if (IsDirectory)
            {
                Directory.CreateDirectory(Path);
            }
        }

        public void Dispose()
        {
            if (File.Exists(Path) || Directory.Exists(Path))
            {
                FileUtil.DeleteFileOrDirectory(Path);
            }
        }
    }
}
