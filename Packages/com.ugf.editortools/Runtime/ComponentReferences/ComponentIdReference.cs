using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.FileIds;
using UnityEngine;

namespace UGF.EditorTools.Runtime.ComponentReferences
{
    [Serializable]
    public struct ComponentIdReference<TComponent> where TComponent : Component
    {
        [SerializeField] private FileId m_fileId;
        [SerializeField] private TComponent m_component;

        public FileId FileId { get { return HasFileId ? m_fileId : throw new ArgumentException("Value not specified."); } }
        public bool HasFileId { get { return m_fileId.IsValid(); } }
        public TComponent Component { get { return HasComponent ? m_component : throw new ArgumentException("Value not specified."); } }
        public bool HasComponent { get { return m_component != null; } }

        public ComponentIdReference(FileId fileId, TComponent component)
        {
            if (!fileId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(fileId));

            m_fileId = fileId;
            m_component = component ? component : throw new ArgumentNullException(nameof(component));
        }

        public bool IsValid()
        {
            return m_fileId.IsValid() && m_component != null;
        }

        public bool Equals(ComponentIdReference<TComponent> other)
        {
            return m_fileId == other.m_fileId && EqualityComparer<TComponent>.Default.Equals(m_component, other.m_component);
        }

        public override bool Equals(object obj)
        {
            return obj is ComponentIdReference<TComponent> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(m_fileId, m_component);
        }

        public static bool operator ==(ComponentIdReference<TComponent> first, ComponentIdReference<TComponent> second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(ComponentIdReference<TComponent> first, ComponentIdReference<TComponent> second)
        {
            return !first.Equals(second);
        }

        public static implicit operator FileId(ComponentIdReference<TComponent> reference)
        {
            return reference.FileId;
        }

        public static implicit operator TComponent(ComponentIdReference<TComponent> reference)
        {
            return reference.Component;
        }
    }
}
