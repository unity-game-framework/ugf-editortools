using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.Preferences
{
    public static class PreferencesEditorUtility
    {
        public static T GetValue<T>(string key, T defaultValue = default)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or empty.", nameof(key));

            string text = EditorPrefs.GetString(key);

            if (!string.IsNullOrEmpty(text))
            {
                var data = new PreferenceEditorValueData<T>();

                EditorJsonUtility.FromJsonOverwrite(text, data);

                return data.Value;
            }

            return defaultValue;
        }

        public static void SetValue<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            if (value == null) throw new ArgumentNullException(nameof(value));

            var data = new PreferenceEditorValueData<T> { Value = value };
            string text = EditorJsonUtility.ToJson(data);

            EditorPrefs.SetString(key, text);
        }
    }
}
