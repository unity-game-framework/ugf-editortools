using System;
using UnityEngine;

namespace UGF.EditorTools.Runtime.IMGUI.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SelectFileAttribute : PropertyAttribute
    {
        public string Title { get; }
        public string Directory { get; }
        public string Extension { get; }
        public bool InAssets { get; }

        public SelectFileAttribute() : this("*")
        {
        }

        public SelectFileAttribute(string extension) : this("Select File", "Assets", extension)
        {
        }

        public SelectFileAttribute(string directory, string extension, bool inAssets = true) : this("Select File", directory, extension, inAssets)
        {
        }

        public SelectFileAttribute(string title, string directory, string extension, bool inAssets = true)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Value cannot be null or empty.", nameof(title));
            if (string.IsNullOrEmpty(directory)) throw new ArgumentException("Value cannot be null or empty.", nameof(directory));
            if (string.IsNullOrEmpty(extension)) throw new ArgumentException("Value cannot be null or empty.", nameof(extension));

            Title = title;
            Directory = directory;
            Extension = extension;
            InAssets = inAssets;
        }
    }
}
