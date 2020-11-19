using System;
using UnityEngine;

namespace UGF.EditorTools.Runtime.IMGUI.Types
{
    [Serializable]
    public struct TypeReference : IEquatable<TypeReference>, IComparable<TypeReference>
    {
        [SerializeField] private string m_value;

        public string Value { get { return HasValue ? m_value : throw new ArgumentException("Value not specified."); } set { m_value = value; } }
        public bool HasValue { get { return !string.IsNullOrEmpty(m_value); } }

        public TypeReference(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            m_value = value;
        }

        public Type Get()
        {
            return TryGet(out Type type) ? type : throw new ArgumentException($"Type not found by the specified type name: '{m_value}'.");
        }

        public bool TryGet(out Type type)
        {
            type = Type.GetType(m_value);

            return type != null;
        }

        public void Set(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            m_value = type.AssemblyQualifiedName;
        }

        public void Clear()
        {
            m_value = string.Empty;
        }

        public bool Equals(TypeReference other)
        {
            return m_value == other.m_value;
        }

        public override bool Equals(object obj)
        {
            return obj is TypeReference other && Equals(other);
        }

        public override int GetHashCode()
        {
            return m_value != null ? m_value.GetHashCode() : 0;
        }

        public int CompareTo(TypeReference other)
        {
            return string.Compare(m_value, other.m_value, StringComparison.Ordinal);
        }

        public static bool operator ==(TypeReference first, TypeReference second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(TypeReference first, TypeReference second)
        {
            return !first.Equals(second);
        }

        public override string ToString()
        {
            return $"{nameof(m_value)}: {m_value}";
        }
    }
}
