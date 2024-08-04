using System;
using UnityEngine;

namespace UGF.EditorTools.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ListKeyAndValueAttribute : PropertyAttribute
    {
        public string PropertyKeyName { get; }
        public string PropertyValueName { get; }
        public bool DisplayLabels { get; set; }
        public string KeyLabel { get; set; }
        public string ValueLabel { get; set; }

        public ListKeyAndValueAttribute(string propertyKeyName = "m_key", string propertyValueName = "m_value") : base(true)
        {
            if (string.IsNullOrEmpty(propertyKeyName)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyKeyName));
            if (string.IsNullOrEmpty(propertyValueName)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyValueName));

            PropertyKeyName = propertyKeyName;
            PropertyValueName = propertyValueName;
        }
    }
}
