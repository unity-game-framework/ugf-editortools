using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.AssetReferences
{
    [CreateAssetMenu(menuName = "Tests/TestAssetReferenceAsset2")]
    public class TestAssetReferenceAsset2 : ScriptableObject
    {
        [SerializeField] private AssetReference<ScriptableObject> m_scriptable;
        [SerializeField] private AssetReference<Material> m_material;
        [SerializeField] private List<AssetReference<Material>> m_list = new List<AssetReference<Material>>();
        [SerializeField] private List<AssetReference<Material>> m_list2 = new List<AssetReference<Material>>();

        public AssetReference<ScriptableObject> Scriptable { get { return m_scriptable; } set { m_scriptable = value; } }
        public AssetReference<Material> Material { get { return m_material; } set { m_material = value; } }
        public List<AssetReference<Material>> List { get { return m_list; } }
        public List<AssetReference<Material>> List2 { get { return m_list2; } }
    }
}
