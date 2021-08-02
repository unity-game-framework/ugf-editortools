using System;
using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;

namespace UGF.EditorTools.Editor.Preferences
{
    public abstract class PreferenceEditorValue : DrawerBase
    {
        public string Key { get; }

        protected PreferenceEditorValue(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or empty.", nameof(key));

            Key = key;
        }

        public void Apply()
        {
            OnApply();
        }

        public void Revert()
        {
            OnRevert();
        }

        public bool Exists()
        {
            return OnExists();
        }

        public void Clear()
        {
            OnClear();
        }

        public object GetValue()
        {
            return OnGetValue();
        }

        public void SetValue(object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            OnSetValue(value);
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

        protected abstract void OnApply();
        protected abstract void OnRevert();

        protected virtual bool OnExists()
        {
            return EditorPrefs.HasKey(Key);
        }

        protected virtual void OnClear()
        {
            EditorPrefs.DeleteKey(Key);
        }

        protected abstract object OnGetValue();
        protected abstract void OnSetValue(object value);
    }
}
