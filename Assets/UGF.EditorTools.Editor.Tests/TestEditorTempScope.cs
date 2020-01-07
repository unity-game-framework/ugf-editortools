using System.IO;
using NUnit.Framework;

namespace UGF.EditorTools.Editor.Tests
{
    public class TestEditorTempScope
    {
        [Test]
        public void Create()
        {
            string path = string.Empty;

            using (var scope = new EditorTempScope(true))
            {
                Assert.NotNull(scope.Path);
                Assert.IsNotEmpty(scope.Path);
                Assert.True(Directory.Exists(scope.Path));

                path = scope.Path;
            }

            Assert.False(Directory.Exists(path));
        }
    }
}
