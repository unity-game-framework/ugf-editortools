using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.EditorTools.Runtime.IMGUI.EnabledProperty
{
    [Serializable]
    public struct EnabledProperty<TValue>
    {
        [SerializeField] private bool m_enabled;
        [SerializeField] private TValue m_value;

        public bool Enabled { get { return m_enabled; } set { m_enabled = value; } }
        public TValue Value { get { return m_value; } set { m_value = value; } }

        public EnabledProperty(TValue value) : this()
        {
            m_enabled = true;
            m_value = value;
        }

        public EnabledProperty(bool enabled, TValue value)
        {
            m_enabled = enabled;
            m_value = value;
        }

        public bool Equals(EnabledProperty<TValue> other)
        {
            return EqualityComparer<TValue>.Default.Equals(m_value, other.m_value);
        }

        public override bool Equals(object obj)
        {
            return obj is EnabledProperty<TValue> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TValue>.Default.GetHashCode(m_value);
        }

        public static bool operator ==(EnabledProperty<TValue> first, EnabledProperty<TValue> second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(EnabledProperty<TValue> first, EnabledProperty<TValue> second)
        {
            return !first.Equals(second);
        }

        public static implicit operator bool(EnabledProperty<TValue> property)
        {
            return property.m_enabled;
        }

        public static implicit operator TValue(EnabledProperty<TValue> property)
        {
            return property.m_value;
        }

        public override string ToString()
        {
            return $"{m_value} (Enabled: {m_enabled})";
        }
    }
}
