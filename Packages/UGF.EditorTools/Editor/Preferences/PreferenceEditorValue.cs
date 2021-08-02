using System;
using UGF.EditorTools.Editor.IMGUI;

namespace UGF.EditorTools.Editor.Preferences
{
    public class PreferenceEditorValue<TValue> : DrawerBase
    {
        public string Key { get; }
        public TValue DefaultValue { get; }
        public TValue Value { get; set; }

        public PreferenceEditorValue(string key, TValue defaultValue = default)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or empty.", nameof(key));

            Key = key;
            DefaultValue = defaultValue ?? throw new ArgumentNullException(nameof(defaultValue));
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            Revert();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            Apply();
        }

        public void Apply()
        {
            PreferencesEditorUtility.SetValue(Key, Value);
        }

        public void Revert()
        {
            Value = PreferencesEditorUtility.GetValue(Key, DefaultValue);
        }
    }
}
