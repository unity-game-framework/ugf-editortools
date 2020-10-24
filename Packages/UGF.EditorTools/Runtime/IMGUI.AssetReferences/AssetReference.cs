﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Runtime.IMGUI.AssetReferences
{
    [Serializable]
    public struct AssetReference<TAsset> where TAsset : Object
    {
        [SerializeField] private string m_guid;
        [SerializeField] private TAsset m_asset;

        public string Guid { get { return !string.IsNullOrEmpty(m_guid) ? m_guid : throw new ArgumentException("Asset guid not specified."); } set { m_guid = value; } }
        public bool HasGuid { get { return !string.IsNullOrEmpty(m_guid); } }
        public TAsset Asset { get { return m_asset ? m_asset : throw new ArgumentException("Asset not specified."); } set { m_asset = value; } }
        public bool HasAsset { get { return m_asset != null; } }

        public AssetReference(string guid, TAsset asset)
        {
            m_guid = guid;
            m_asset = asset;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(m_guid) && m_asset != null;
        }

        public bool Equals(AssetReference<TAsset> other)
        {
            return m_guid == other.m_guid && EqualityComparer<TAsset>.Default.Equals(m_asset, other.m_asset);
        }

        public override bool Equals(object obj)
        {
            return obj is AssetReference<TAsset> other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((m_guid != null ? m_guid.GetHashCode() : 0) * 397) ^ EqualityComparer<TAsset>.Default.GetHashCode(m_asset);
            }
        }

        public static bool operator ==(AssetReference<TAsset> first, AssetReference<TAsset> second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(AssetReference<TAsset> first, AssetReference<TAsset> second)
        {
            return !first.Equals(second);
        }

        public static implicit operator string(AssetReference<TAsset> reference)
        {
            return reference.m_guid;
        }

        public static implicit operator TAsset(AssetReference<TAsset> reference)
        {
            return reference.m_asset;
        }

        public override string ToString()
        {
            return $"{m_asset} (Guid: '{m_guid}')";
        }
    }
}
