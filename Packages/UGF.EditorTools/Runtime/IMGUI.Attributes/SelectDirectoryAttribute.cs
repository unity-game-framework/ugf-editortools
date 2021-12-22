using System;
using UnityEngine;

namespace UGF.EditorTools.Runtime.IMGUI.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SelectDirectoryAttribute : PropertyAttribute
    {
        public string Title { get; }
        public string Directory { get; }
        public bool InAssets { get; }

        public SelectDirectoryAttribute() : this("Assets")
        {
        }

        public SelectDirectoryAttribute(string directory, bool inAssets = true) : this("Select Directory", directory, inAssets)
        {
        }

        public SelectDirectoryAttribute(string title, string directory, bool inAssets = true)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Value cannot be null or empty.", nameof(title));
            if (string.IsNullOrEmpty(directory)) throw new ArgumentException("Value cannot be null or empty.", nameof(directory));

            Title = title;
            Directory = directory;
            InAssets = inAssets;
        }
    }
}
