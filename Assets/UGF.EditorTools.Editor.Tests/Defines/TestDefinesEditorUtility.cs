using NUnit.Framework;
using UGF.EditorTools.Editor.Defines;
using UnityEditor;

namespace UGF.EditorTools.Editor.Tests.Defines
{
    public class TestDefinesEditorUtility
    {
        [Test]
        public void SetDefine()
        {
            DefinesEditorUtility.SetDefine("TEST", BuildTargetGroup.Standalone);
        }
    }
}
