using System;
using UnityEngine;

namespace UGF.EditorTools.Runtime.FileIds
{
    [Serializable]
    public struct FileId : IEquatable<FileId>, IComparable<FileId>
    {
        [SerializeField] private ulong m_value;

        public ulong Value { get { return m_value; } set { m_value = value; } }

        public FileId(ulong value)
        {
            m_value = value;
        }

        public bool IsValid()
        {
            return m_value > 0U;
        }

        public bool Equals(FileId other)
        {
            return m_value == other.m_value;
        }

        public override bool Equals(object obj)
        {
            return obj is FileId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }

        public int CompareTo(FileId other)
        {
            return m_value.CompareTo(other.m_value);
        }

        public static bool operator ==(FileId first, FileId second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(FileId first, FileId second)
        {
            return !first.Equals(second);
        }

        public static implicit operator ulong(FileId fileId)
        {
            return fileId.Value;
        }

        public static implicit operator long(FileId fileId)
        {
            return (long)fileId.Value;
        }

        public static implicit operator FileId(ulong value)
        {
            return new FileId(value);
        }

        public static implicit operator FileId(long value)
        {
            return new FileId((ulong)value);
        }

        public override string ToString()
        {
            return m_value.ToString();
        }
    }
}
