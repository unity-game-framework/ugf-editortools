using System;
using UnityEngine;

namespace UGF.EditorTools.Runtime.IMGUI.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SelectDirectoryAttribute : PropertyAttribute
    {
        public string Title { get; }
        public string DefaultDirectory { get; }
        public bool Relative { get; }

        public SelectDirectoryAttribute() : this("Assets")
        {
        }

        public SelectDirectoryAttribute(string defaultDirectory, bool relative = true) : this("Select Directory", defaultDirectory, relative)
        {
        }

        public SelectDirectoryAttribute(string title, string defaultDirectory, bool relative = true)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Value cannot be null or empty.", nameof(title));
            if (string.IsNullOrEmpty(defaultDirectory)) throw new ArgumentException("Value cannot be null or empty.", nameof(defaultDirectory));

            Title = title;
            DefaultDirectory = defaultDirectory;
            Relative = relative;
        }
    }
}
