using UGF.EditorTools.Runtime.Platforms;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Platforms
{
    [CreateAssetMenu(menuName = "Tests/TestPlatformsSelectionAsset")]
    public class TestPlatformSelectionAsset : ScriptableObject
    {
        [SerializeField] private PlatformSettings<BuildTargetGroup> m_editor = new PlatformSettings<BuildTargetGroup>();
        [SerializeField] private PlatformSettings<BuildTarget> m_editor2 = new PlatformSettings<BuildTarget>();
        [SerializeField] private PlatformSettings<RuntimePlatform> m_runtime = new PlatformSettings<RuntimePlatform>();

        public PlatformSettings<BuildTargetGroup> Editor { get { return m_editor; } }
        public PlatformSettings<BuildTarget> Editor2 { get { return m_editor2; } }
        public PlatformSettings<RuntimePlatform> Runtime { get { return m_runtime; } }
    }
}
