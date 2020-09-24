using NUnit.Framework;
using UGF.EditorTools.Editor.Assets;

namespace UGF.EditorTools.Editor.Tests.Assets
{
    public class TestAssetsEditorUtility
    {
        [Test]
        public void TryGetResourcesRelativePath()
        {
            string path1 = "Assets/Resources/Test/Folder/test-test.asset";
            string path12 = "Assets/Resources/Test/Folder/resources.asset";
            string path13 = "Assets/Resources/Test/Resources/Folder/resources.asset";
            string path14 = "Assets/UGF.EditorTools.Editor.Tests/IMGUI.Attributes/New Material.mat";
            string path2 = "Resources/Folder";
            string path3 = "Assets/Resources/";

            bool result1 = AssetsEditorUtility.TryGetResourcesRelativePath(path1, out string pathResult1);
            bool result12 = AssetsEditorUtility.TryGetResourcesRelativePath(path12, out string pathResult12);
            bool result13 = AssetsEditorUtility.TryGetResourcesRelativePath(path13, out string pathResult13);
            bool result14 = AssetsEditorUtility.TryGetResourcesRelativePath(path14, out string pathResult14);
            bool result2 = AssetsEditorUtility.TryGetResourcesRelativePath(path2, out string pathResult2);
            bool result3 = AssetsEditorUtility.TryGetResourcesRelativePath(path3, out string pathResult3);

            Assert.True(result1, $"pathResult1: {pathResult1}");
            Assert.True(result12, $"pathResult12: {pathResult12}");
            Assert.True(result13, $"pathResult13: {pathResult13}");
            Assert.False(result14, $"pathResult14: {pathResult14}");
            Assert.True(result2, $"pathResult2: {pathResult2}");
            Assert.False(result3, $"pathResult3: {pathResult3}");
            Assert.AreEqual("Test/Folder/test-test", pathResult1);
            Assert.AreEqual("Test/Folder/resources", pathResult12);
            Assert.AreEqual("Folder/resources", pathResult13);
            Assert.AreEqual("Folder", pathResult2);
            Assert.AreEqual(null, pathResult3);
        }
    }
}
