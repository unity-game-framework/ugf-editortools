using System;
using System.Reflection;
using UnityEngine;

namespace UGF.EditorTools.Runtime.IMGUI.Types
{
    [Serializable]
    public struct AssemblyReference : IEquatable<AssemblyReference>, IComparable<AssemblyReference>
    {
        [SerializeField] private string m_value;

        public string Value { get { return HasValue ? m_value : throw new ArgumentException("Value not specified."); } set { m_value = value; } }
        public bool HasValue { get { return !string.IsNullOrEmpty(m_value); } }

        public AssemblyReference(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            m_value = value;
        }

        public Assembly Get()
        {
            return AssemblyUtility.GetAssemblyByFullName(m_value);
        }

        public void Set(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            m_value = assembly.FullName;
        }

        public void Clear()
        {
            m_value = string.Empty;
        }

        public bool Equals(AssemblyReference other)
        {
            return m_value == other.m_value;
        }

        public override bool Equals(object obj)
        {
            return obj is AssemblyReference other && Equals(other);
        }

        public override int GetHashCode()
        {
            return m_value != null ? m_value.GetHashCode() : 0;
        }

        public int CompareTo(AssemblyReference other)
        {
            return string.Compare(m_value, other.m_value, StringComparison.Ordinal);
        }

        public static bool operator ==(AssemblyReference first, AssemblyReference second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(AssemblyReference first, AssemblyReference second)
        {
            return !first.Equals(second);
        }

        public override string ToString()
        {
            return $"{nameof(m_value)}: {m_value}";
        }
    }
}
