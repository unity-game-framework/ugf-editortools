using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UGF.EditorTools.Runtime.Ids
{
    [Serializable]
    public struct GlobalId : IEquatable<GlobalId>, IComparable<GlobalId>
    {
        [SerializeField] private ulong m_first;
        [SerializeField] private ulong m_second;

        public ulong First { get { return m_first; } set { m_first = value; } }
        public ulong Second { get { return m_second; } set { m_second = value; } }
        public bool IsEmpty { get { return m_first == 0U && m_second == 0U; } }

        public static GlobalId Empty { get; } = new GlobalId(0U, 0U);

        [StructLayout(LayoutKind.Explicit)]
        private struct Converter
        {
            [FieldOffset(0)] public Guid Guid;
            [FieldOffset(0)] public GlobalId Id;
        }

        public GlobalId(ulong first, ulong second)
        {
            m_first = first;
            m_second = second;
        }

        public GlobalId(string value)
        {
            GlobalId id = Parse(value);

            m_first = id.m_first;
            m_second = id.m_second;
        }

        public bool IsValid()
        {
            return m_first > 0U && m_second > 0U;
        }

        public bool Equals(GlobalId other)
        {
            return m_first == other.m_first && m_second == other.m_second;
        }

        public override bool Equals(object obj)
        {
            return obj is GlobalId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(m_first, m_second);
        }

        public int CompareTo(GlobalId other)
        {
            int firstComparison = m_first.CompareTo(other.m_first);

            return firstComparison == 0 ? m_second.CompareTo(other.m_second) : firstComparison;
        }

        public static GlobalId Parse(string value)
        {
            return TryParse(value, out GlobalId id) ? id : throw new ArgumentException($"Can not parse specified value: '{value}'.");
        }

        public static bool TryParse(string value, out GlobalId id)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            if (Guid.TryParse(value, out Guid guid))
            {
                id = FromGuid(guid);
                return true;
            }

            id = default;
            return false;
        }

        public static GlobalId Generate()
        {
            var guid = Guid.NewGuid();

            return FromGuid(guid);
        }

        public static GlobalId FromGuid(Guid guid)
        {
            var converter = new Converter { Guid = guid };

            return converter.Id;
        }

        public static Guid ToGuid(GlobalId id)
        {
            var converter = new Converter { Id = id };

            return converter.Guid;
        }

        public static bool operator ==(GlobalId first, GlobalId second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(GlobalId first, GlobalId second)
        {
            return !first.Equals(second);
        }

        public static implicit operator Guid(GlobalId id)
        {
            return ToGuid(id);
        }

        public static implicit operator GlobalId(Guid guid)
        {
            return FromGuid(guid);
        }

        public static implicit operator string(GlobalId id)
        {
            return id.ToString();
        }

        public static implicit operator GlobalId(string value)
        {
            return new GlobalId(value);
        }

        public string ToString(string format)
        {
            if (string.IsNullOrEmpty(format)) throw new ArgumentException("Value cannot be null or empty.", nameof(format));

            Guid guid = ToGuid(this);

            return guid.ToString(format);
        }

        public override string ToString()
        {
            return ToString("N");
        }
    }
}
