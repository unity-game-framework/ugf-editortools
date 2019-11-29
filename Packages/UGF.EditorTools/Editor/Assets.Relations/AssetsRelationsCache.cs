using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace UGF.EditorTools.Editor.Assets.Relations
{
    public class AssetsRelationsCache
    {
        public string Path { get; }
        public Dictionary<string, HashSet<string>> Assets { get; } = new Dictionary<string, HashSet<string>>();
        public bool Readable { get; set; } = true;

        public AssetsRelationsCache(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            Path = path;
        }

        public void Save()
        {
            var data = new AssetsRelationsData();

            foreach (KeyValuePair<string, HashSet<string>> pair in Assets)
            {
                var relation = new AssetsRelation(pair.Key, pair.Value);

                data.Relations.Add(relation);
            }

            string text = ToJson(data, Readable);

            File.WriteAllText(Path, text);
        }

        public void Load()
        {
            string text = File.ReadAllText(Path);
            AssetsRelationsData data = FromJson(text);

            for (int i = 0; i < data.Relations.Count; i++)
            {
                AssetsRelation relation = data.Relations[i];

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

        private static string ToJson(AssetsRelationsData data, bool readable = true)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            string text = EditorJsonUtility.ToJson(data, readable);

            return text;
        }

        private static AssetsRelationsData FromJson(string text)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentException("Value cannot be null or empty.", nameof(text));

            var data = new AssetsRelationsData();

            EditorJsonUtility.FromJsonOverwrite(text, data);

            return data;
        }
    }
}
