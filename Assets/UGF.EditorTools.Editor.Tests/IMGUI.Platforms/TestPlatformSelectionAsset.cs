using UGF.EditorTools.Editor.IMGUI.Platforms;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Platforms
{
    [CreateAssetMenu(menuName = "Tests/TestPlatformsSelectionAsset")]
    public class TestPlatformSelectionAsset : ScriptableObject
    {
        [SerializeField] private PlatformSettings m_settings = new PlatformSettings();

        public PlatformSettings Settings { get { return m_settings; } }
    }
}
