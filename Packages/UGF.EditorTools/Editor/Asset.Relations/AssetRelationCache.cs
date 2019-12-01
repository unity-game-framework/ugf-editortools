using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace UGF.EditorTools.Editor.Asset.Relations
{
    public class AssetRelationCache
    {
        public string Path { get; }
        public Dictionary<string, HashSet<string>> Assets { get; } = new Dictionary<string, HashSet<string>>();
        public bool Readable { get; set; } = true;

        public AssetRelationCache(string packageName, string cacheName)
        {
            if (string.IsNullOrEmpty(packageName)) throw new ArgumentException("Value cannot be null or empty.", nameof(packageName));
            if (string.IsNullOrEmpty(cacheName)) throw new ArgumentException("Value cannot be null or empty.", nameof(cacheName));

            Path = $"ProjectSettings/Packages/{packageName}/{cacheName}.json";
        }

        public AssetRelationCache(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            Path = path;
        }

        public void Add(string parent, string child)
        {
            if (!Assets.TryGetValue(parent, out HashSet<string> children))
            {
                children = new HashSet<string>();

                Assets.Add(parent, children);
            }

            children.Add(child);
        }

        public void Remove(string parent, string child)
        {
            if (Assets.TryGetValue(parent, out HashSet<string> children))
            {
                children.Remove(child);

                if (children.Count == 0)
                {
                    Assets.Remove(parent);
                }
            }
        }

        public void Remove(string parent)
        {
            Assets.Remove(parent);
        }

        public void Save()
        {
            var data = new AssetRelationData();

            foreach (KeyValuePair<string, HashSet<string>> pair in Assets)
            {
                var relation = new AssetRelation(pair.Key, pair.Value);

                data.Relations.Add(relation);
            }

            string text = ToJson(data, Readable);

            File.WriteAllText(Path, text);
        }

        public void Load()
        {
            string text = File.ReadAllText(Path);
            AssetRelationData data = FromJson(text);

            for (int i = 0; i < data.Relations.Count; i++)
            {
                AssetRelation relation = data.Relations[i];

                if (!Assets.TryGetValue(relation.Parent, out HashSet<string> children))
                {
                    children = new HashSet<string>();

                    Assets.Add(relation.Parent, children);
                }

                for (int c = 0; c < relation.Children.Count; c++)
                {
                    children.Add(relation.Children[i]);
                }
            }
        }

        private static string ToJson(AssetRelationData data, bool readable = true)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            string text = EditorJsonUtility.ToJson(data, readable);

            return text;
        }

        private static AssetRelationData FromJson(string text)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentException("Value cannot be null or empty.", nameof(text));

            var data = new AssetRelationData();

            EditorJsonUtility.FromJsonOverwrite(text, data);

            return data;
        }
    }
}
