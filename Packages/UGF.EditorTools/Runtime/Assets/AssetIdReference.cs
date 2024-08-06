using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Runtime.Assets
{
    [Serializable]
    public struct AssetIdReference<TAsset> where TAsset : Object
    {
        [SerializeField] private Hash128 m_guid;
        [SerializeField] private TAsset m_asset;

        public GlobalId Guid { get { return m_guid.isValid ? m_guid : throw new ArgumentException("Asset guid not specified."); } set { m_guid = value; } }
        public bool HasGuid { get { return m_guid.isValid; } }
        public TAsset Asset { get { return m_asset ? m_asset : throw new ArgumentException("Asset not specified."); } set { m_asset = value; } }
        public bool HasAsset { get { return m_asset != null; } }

        public AssetIdReference(GlobalId guid, TAsset asset)
        {
            if (!guid.IsValid()) throw new ArgumentException("Value should be valid.", nameof(guid));

            m_guid = guid;
            m_asset = asset ? asset : throw new ArgumentNullException(nameof(asset));
        }

        public bool IsValid()
        {
            return m_guid.isValid && m_asset != null;
        }

        public bool Equals(AssetIdReference<TAsset> other)
        {
            return m_guid.Equals(other.m_guid) && EqualityComparer<TAsset>.Default.Equals(m_asset, other.m_asset);
        }

        public override bool Equals(object obj)
        {
            return obj is AssetIdReference<TAsset> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(m_guid, m_asset);
        }

        public static bool operator ==(AssetIdReference<TAsset> first, AssetIdReference<TAsset> second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(AssetIdReference<TAsset> first, AssetIdReference<TAsset> second)
        {
            return !first.Equals(second);
        }

        public static implicit operator GlobalId(AssetIdReference<TAsset> reference)
        {
            return reference.Guid;
        }

        public static implicit operator TAsset(AssetIdReference<TAsset> reference)
        {
            return reference.Asset;
        }

        public override string ToString()
        {
            return $"{m_asset} (Guid: '{m_guid}')";
        }
    }
}
