using UGF.EditorTools.Runtime.Platforms;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Platforms
{
    [CreateAssetMenu(menuName = "Tests/TestPlatformsSelectionAsset")]
    public class TestPlatformSelectionAsset : ScriptableObject
    {
        [SerializeField] private PlatformSettings<PlatformGroup<BuildTargetGroup>> m_editor = new PlatformSettings<PlatformGroup<BuildTargetGroup>>();
        [SerializeField] private PlatformSettings<PlatformGroup<BuildTarget>> m_editor2 = new PlatformSettings<PlatformGroup<BuildTarget>>();
        [SerializeField] private PlatformSettings<PlatformGroup<RuntimePlatform>> m_runtime = new PlatformSettings<PlatformGroup<RuntimePlatform>>();

        public PlatformSettings<PlatformGroup<BuildTargetGroup>> Editor { get { return m_editor; } }
        public PlatformSettings<PlatformGroup<BuildTarget>> Editor2 { get { return m_editor2; } }
        public PlatformSettings<PlatformGroup<RuntimePlatform>> Runtime { get { return m_runtime; } }
    }
}
