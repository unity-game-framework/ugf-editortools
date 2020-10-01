using NUnit.Framework;
using UGF.EditorTools.Runtime.IMGUI.EnabledProperty;

namespace UGF.EditorTools.Editor.Tests.IMGUI.EnabledProperty
{
    public class TestEnabledProperty
    {
        [Test]
        public void Equals()
        {
            var a = new EnabledProperty<int>(10);
            var b = new EnabledProperty<int>(10);
            var c = new EnabledProperty<int>(11);

            bool result1 = a.Equals(b);
            bool result2 = a.Equals(c);
            bool result3 = a.Equals(10);

            Assert.True(result1);
            Assert.False(result2);
            Assert.False(result3);
        }

        [Test]
        public void OperatorEqual()
        {
            var a = new EnabledProperty<int>(10);
            var b = new EnabledProperty<int>(10);
            var c = new EnabledProperty<int>(11);

            bool result1 = a == b;
            bool result2 = a == c;
            bool result3 = a == 10;

            Assert.True(result1);
            Assert.False(result2);
            Assert.True(result3);
        }

        [Test]
        public void OperatorEqual2()
        {
            var a = new EnabledProperty<int>(true, 10);
            var b = new EnabledProperty<int>(false, 11);

            bool result1 = a;
            bool result2 = b;

            Assert.True(result1);
            Assert.False(result2);
        }
    }
}
