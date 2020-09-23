using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Attributes
{
    [CreateAssetMenu(menuName = "Tests/TestAttributesAsset")]
    public class TestAttributesAsset : ScriptableObject, ITestAttributesAsset
    {
        [SerializeField, Disable] private string m_disabled = "Text";
        [SerializeField, AssetGuid] private string m_assetGuid;

        [SerializeField, AssetGuid(typeof(Material))]
        private string m_guidMaterial;

        [SerializeField, AssetGuid(typeof(Scene))]
        private string m_guidScene;

        [SerializeField, AssetType] private Object m_typeObject;

        [SerializeField, AssetType(typeof(ITestAttributesAsset))]
        private Object m_typeInterface;

        [SerializeField, AssetPath(typeof(Scene))]
        private string m_pathMaterial;

        [SerializeField, AssetPath(typeof(Material))]
        private string m_pathScene;

        public string Disabled { get { return m_disabled; } set { m_disabled = value; } }
        public string AssetGuid { get { return m_assetGuid; } set { m_assetGuid = value; } }
        public string GuidMaterial { get { return m_guidMaterial; } set { m_guidMaterial = value; } }
        public string GuidScene { get { return m_guidScene; } set { m_guidScene = value; } }
        public Object TypeObject { get { return m_typeObject; } set { m_typeObject = value; } }
        public Object TypeInterface { get { return m_typeInterface; } set { m_typeInterface = value; } }
        public string PathMaterial { get { return m_pathMaterial; } set { m_pathMaterial = value; } }
        public string PathScene { get { return m_pathScene; } set { m_pathScene = value; } }
    }

    public interface ITestAttributesAsset
    {
    }
}
