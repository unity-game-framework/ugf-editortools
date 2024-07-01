namespace UGF.EditorTools.Editor.Preferences
{
    public class PreferenceEditorValue<TValue> : PreferenceEditorValue
    {
        public TValue DefaultValue { get; }
        public TValue Value { get; set; }

        public PreferenceEditorValue(string key, TValue defaultValue = default) : base(key)
        {
            DefaultValue = defaultValue;
        }

        protected override void OnApply()
        {
            PreferencesEditorUtility.SetValue(Key, Value);
        }

        protected override void OnRevert()
        {
            Value = PreferencesEditorUtility.GetValue(Key, DefaultValue);
        }

        protected override object OnGetValue()
        {
            return Value;
        }

        protected override void OnSetValue(object value)
        {
            Value = (TValue)value;
        }
    }
}
