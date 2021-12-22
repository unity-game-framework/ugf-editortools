using System;
using UnityEngine;

namespace UGF.EditorTools.Runtime.IMGUI.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SelectFileAttribute : PropertyAttribute
    {
        public string Title { get; }
        public string DefaultDirectory { get; }
        public string Extension { get; }
        public bool InAssets { get; }

        public SelectFileAttribute() : this("*")
        {
        }

        public SelectFileAttribute(string extension) : this("Select File", "Assets", extension)
        {
        }

        public SelectFileAttribute(string defaultDirectory, string extension, bool inAssets = true) : this("Select File", defaultDirectory, extension, inAssets)
        {
        }

        public SelectFileAttribute(string title, string defaultDirectory, string extension, bool inAssets = true)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Value cannot be null or empty.", nameof(title));
            if (string.IsNullOrEmpty(defaultDirectory)) throw new ArgumentException("Value cannot be null or empty.", nameof(defaultDirectory));
            if (string.IsNullOrEmpty(extension)) throw new ArgumentException("Value cannot be null or empty.", nameof(extension));

            Title = title;
            DefaultDirectory = defaultDirectory;
            Extension = extension;
            InAssets = inAssets;
        }
    }
}
