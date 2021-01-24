using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UGF.EditorTools.Editor.Platforms;

namespace UGF.EditorTools.Editor.Tests.Platforms
{
    public class TestPlatformEditorUtility
    {
        [Test]
        public void PlatformsAll()
        {
            Assert.Pass(PrintPlatforms(PlatformEditorUtility.PlatformsAll));
        }

        [Test]
        public void PlatformsAllAvailable()
        {
            Assert.Pass(PrintPlatforms(PlatformEditorUtility.PlatformsAllAvailable));
        }

        private string PrintPlatforms(IReadOnlyList<PlatformInfo> platforms)
        {
            var builder = new StringBuilder();

            foreach (PlatformInfo platform in platforms)
            {
                builder.AppendLine(platform.Name);
                builder.AppendLine($"  {platform.BuildTargetGroup}");
                builder.AppendLine($"  {platform.BuildTarget}");
                builder.AppendLine($"  {platform.Label.text}");
                builder.AppendLine($"  {platform.Label.tooltip}");
                builder.AppendLine($"  {platform.Label.image.name}");
            }

            return builder.ToString();
        }
    }
}
