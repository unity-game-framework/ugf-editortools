﻿using System;
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
        public bool IsEmpty { get { return m_first == 0UL && m_second == 0UL; } }

        public static GlobalId Empty { get; } = new GlobalId(0UL, 0UL);

        [StructLayout(LayoutKind.Explicit)]
        private struct Converter
        {
            [FieldOffset(0)] public Guid Guid;
            [FieldOffset(0)] public GlobalId Id;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct ConverterHash128
        {
            [FieldOffset(0)] public Hash128 Hash128;
            [FieldOffset(0)] public GlobalId Id;
        }

        public GlobalId(ulong value)
        {
            m_first = value;
            m_second = 0UL;
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
            return m_first > 0UL || m_second > 0UL;
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

        public static GlobalId FromHash128(Hash128 hash128)
        {
            var converter = new ConverterHash128 { Hash128 = hash128 };

            return converter.Id;
        }

        public static Hash128 ToHash128(GlobalId id)
        {
            var converter = new ConverterHash128 { Id = id };

            return converter.Hash128;
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

        public static implicit operator Hash128(GlobalId id)
        {
            return ToHash128(id);
        }

        public static implicit operator GlobalId(Hash128 hash128)
        {
            return FromHash128(hash128);
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
