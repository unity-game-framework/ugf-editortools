using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.UIToolkit;
using UGF.EditorTools.Editor.UIToolkit.Elements;
using UGF.EditorTools.Editor.UIToolkit.SerializedProperties;
using UnityEditor;
using UnityEditor.UIElements;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public class DropdownFieldElement<TValue> : DropdownButtonFieldElement<TValue>
    {
        public SerializedPropertyFieldBinding<TValue> PropertyBinding { get; }
        public DropdownSelection<DropdownItem<TValue>> Selection { get; } = new DropdownSelection<DropdownItem<TValue>>();
        public List<DropdownItem<TValue>> Items { get; } = new List<DropdownItem<TValue>>();
        public DropdownItem<TValue> ItemNone { get; set; } = new DropdownItem<TValue>("None");
        public IEqualityComparer<TValue> ItemValueComparer { get; } = EqualityComparer<TValue>.Default;
        public string ContentValueNoneLabel { get; set; } = "None";
        public string ContentValueUnknownLabelFormat { get; set; } = "Unknown (value:'{0}')";

        public DropdownFieldElement(SerializedProperty serializedProperty, bool field) : this()
        {
            if (field)
            {
                UIToolkitEditorUtility.AddFieldClasses(this);
            }

            bindingPath = serializedProperty.propertyPath;

            PropertyBinding.Bind(serializedProperty);

            this.TrackPropertyValue(serializedProperty);
        }

        public DropdownFieldElement()
        {
            PropertyBinding = new SerializedPropertyFieldBinding<TValue>(this);
            PropertyBinding.Update += Update;
            PropertyBinding.Apply += Apply;
            ButtonElement.clicked += OnDropdownButtonClicked;
            Selection.Dropdown.Selected += OnSelected;
        }

        public override void SetValueWithoutNotify(TValue newValue)
        {
            base.SetValueWithoutNotify(newValue);

            OnUpdateValueContent();
        }

        public void Update(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            OnUpdate(serializedProperty);
            OnUpdateValueContent();
        }

        public void Apply(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            OnApply(serializedProperty);
        }

        public bool TryGetItem(TValue itemValue, out DropdownItem<TValue> item)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                item = Items[i];

                if (ItemValueComparer.Equals(itemValue, item.Value))
                {
                    return true;
                }
            }

            item = default;
            return false;
        }

        protected virtual void OnUpdate(SerializedProperty serializedProperty)
        {
        }

        protected virtual void OnApply(SerializedProperty serializedProperty)
        {
        }

        protected virtual void OnSelected(DropdownItem<TValue> item)
        {
            value = item.Value;
        }

        protected virtual void OnUpdateValueContent()
        {
            ButtonElement.text = TryGetItem(value, out DropdownItem<TValue> item)
                ? item.Name
                : string.Format(ContentValueUnknownLabelFormat, value);
        }

        private void OnDropdownButtonClicked()
        {
            Selection.Show(ButtonElement.worldBound, 0, Items);
        }
    }
}
