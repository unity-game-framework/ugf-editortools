using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.EditorTools.Runtime.ComponentReferences
{
    [Serializable]
    public struct ComponentReference<TComponent> where TComponent : Component
    {
        [SerializeField] private string m_fileId;
        [SerializeField] private TComponent m_component;

        public string FileId { get { return HasFileId ? m_fileId : throw new ArgumentException("Value not specified."); } }
        public bool HasFileId { get { return !string.IsNullOrEmpty(m_fileId); } }
        public TComponent Component { get { return HasComponent ? m_component : throw new ArgumentException("Value not specified."); } }
        public bool HasComponent { get { return m_component != null; } }

        public ComponentReference(string fileId, TComponent component)
        {
            if (string.IsNullOrEmpty(fileId)) throw new ArgumentException("Value cannot be null or empty.", nameof(fileId));

            m_fileId = fileId;
            m_component = component ? component : throw new ArgumentNullException(nameof(component));
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(m_fileId) && m_component != null;
        }

        public bool Equals(ComponentReference<TComponent> other)
        {
            return m_fileId == other.m_fileId && EqualityComparer<TComponent>.Default.Equals(m_component, other.m_component);
        }

        public override bool Equals(object obj)
        {
            return obj is ComponentReference<TComponent> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(m_fileId, m_component);
        }

        public static bool operator ==(ComponentReference<TComponent> first, ComponentReference<TComponent> second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(ComponentReference<TComponent> first, ComponentReference<TComponent> second)
        {
            return !first.Equals(second);
        }

        public static implicit operator string(ComponentReference<TComponent> reference)
        {
            return reference.FileId;
        }

        public static implicit operator TComponent(ComponentReference<TComponent> reference)
        {
            return reference.Component;
        }
    }
}
