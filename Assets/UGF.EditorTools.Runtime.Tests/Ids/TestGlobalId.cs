using System;
using NUnit.Framework;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.EditorTools.Runtime.Tests.Ids
{
    public class TestGlobalId
    {
        [Test]
        public void ConvertGuid()
        {
            Guid guid = Guid.Parse("9d01e359cac54a97a6cb6e6f9d30a69b");
            GlobalId id = GlobalId.FromGuid(guid);

            Assert.AreEqual(guid.ToString("N"), id.ToString());
        }

        [Test]
        public void ConvertHash128()
        {
            Hash128 hash = Hash128.Parse("9d01e359cac54a97a6cb6e6f9d30a69b");
            GlobalId id = GlobalId.FromHash128(hash);
            var hash2 = GlobalId.ToHash128(id);

            Assert.AreEqual(hash.ToString(), id.ToString());
            Assert.AreEqual(hash.ToString(), hash2.ToString());
        }
    }
}
