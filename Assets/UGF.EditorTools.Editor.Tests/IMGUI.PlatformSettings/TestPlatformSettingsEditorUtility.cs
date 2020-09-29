using System.Collections.Generic;
using NUnit.Framework;
using UGF.EditorTools.Editor.IMGUI.PlatformSettings;

namespace UGF.EditorTools.Editor.Tests.IMGUI.PlatformSettings
{
    public class TestPlatformSettingsEditorUtility
    {
        [Test]
        public void GetPlatformsAvailable()
        {
            var platforms = new List<PlatformSettingsInfo>();

            PlatformSettingsEditorUtility.GetPlatformsAvailable(platforms);

            Assert.Greater(platforms.Count, 0);
        }

        [Test]
        public void GetPlatformsAll()
        {
            var platforms = new List<PlatformSettingsInfo>();

            PlatformSettingsEditorUtility.GetPlatformsAll(platforms);

            Assert.Greater(platforms.Count, 0);
        }
    }
}
