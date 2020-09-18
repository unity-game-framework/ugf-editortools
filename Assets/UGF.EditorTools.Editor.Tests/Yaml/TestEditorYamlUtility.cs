using NUnit.Framework;
using UGF.EditorTools.Editor.Yaml;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.Yaml
{
    public class TestEditorYamlUtility
    {
        [Test]
        public void ToYaml()
        {
            var data = ScriptableObject.CreateInstance<TestEditorYamlUtilityData>();
            string yaml = EditorYamlUtility.ToYaml(data);

            Assert.Pass(yaml);
        }

        [Test]
        public void ToYamlAll()
        {
            var data = ScriptableObject.CreateInstance<TestEditorYamlUtilityData>();
            var data2 = ScriptableObject.CreateInstance<TestEditorYamlUtilityData2>();
            string yaml = EditorYamlUtility.ToYamlAll(new Object[] { data, data2 });

            Assert.Pass(yaml);
        }

        [Test]
        public void FromYamlData()
        {
            var data = ScriptableObject.CreateInstance<TestEditorYamlUtilityData>();

            data.Name = "Data Name";

            string yaml = EditorYamlUtility.ToYaml(data);
            var result = EditorYamlUtility.FromYaml<TestEditorYamlUtilityData>(yaml);

            Assert.AreEqual(data.Name, result.Name);
            Assert.Pass(yaml);
        }

        [Test]
        public void FromYamlMaterial()
        {
            Shader shader = Shader.Find("Standard");
            var data = new Material(shader);
            string yaml = EditorYamlUtility.ToYaml(data);

            var result = EditorYamlUtility.FromYaml<Material>(yaml);

            Assert.IsInstanceOf<Material>(result);
            Assert.AreEqual(shader, result.shader);
            Assert.Pass(yaml);
        }

        [Test]
        public void FromYamlAll()
        {
            var data = ScriptableObject.CreateInstance<TestEditorYamlUtilityData>();
            var data2 = ScriptableObject.CreateInstance<TestEditorYamlUtilityData2>();

            data.Name = "Data Name";
            data2.Name = "Data Name 2";

            string yaml = EditorYamlUtility.ToYamlAll(new Object[] { data, data2 });

            Object[] dataAll = EditorYamlUtility.FromYamlAll(yaml);

            Assert.AreEqual(2, dataAll.Length);
            Assert.IsInstanceOf<TestEditorYamlUtilityData>(dataAll[0]);
            Assert.IsInstanceOf<TestEditorYamlUtilityData2>(dataAll[1]);
            Assert.AreEqual(data.Name, ((TestEditorYamlUtilityData)dataAll[0]).Name);
            Assert.AreEqual(data2.Name, ((TestEditorYamlUtilityData2)dataAll[1]).Name);
        }
    }
}
