using NUnit.Framework;
using UGF.EditorTools.Editor.Defines;
using UnityEditor;

namespace UGF.EditorTools.Editor.Tests.Defines
{
    public class TestDefinesEditorUtility
    {
        [TearDown]
        public void CleanUp()
        {
            DefinesEditorUtility.RemoveDefine("TEST", BuildTargetGroup.Android);
        }

        [Test]
        public void HasDefine()
        {
            DefinesEditorUtility.SetDefine("TEST", BuildTargetGroup.Android);

            bool result = DefinesEditorUtility.HasDefine("TEST", BuildTargetGroup.Android);

            Assert.True(result);
        }

        [Test]
        public void RemoveDefine()
        {
            DefinesEditorUtility.SetDefine("TEST", BuildTargetGroup.Android);

            bool result = DefinesEditorUtility.RemoveDefine("TEST", BuildTargetGroup.Android);

            Assert.True(result);
        }

        [Test]
        public void SetDefine()
        {
            bool result = DefinesEditorUtility.SetDefine("TEST", BuildTargetGroup.Android);

            Assert.True(result);
        }
    }
}
