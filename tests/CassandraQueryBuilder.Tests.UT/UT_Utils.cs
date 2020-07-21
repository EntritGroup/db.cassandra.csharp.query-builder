using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CassandraQueryBuilder;

namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_Utils
    {
        [TestMethod]
        public void UT_Utils_CompareStrings()
        {
            Assert.IsTrue(Utils.CompareStrings(null, null));
            Assert.IsFalse(Utils.CompareStrings(null, "asd"));
            Assert.IsFalse(Utils.CompareStrings("asd", null));

            Assert.IsTrue(Utils.CompareStrings("asd", "asd"));
            Assert.IsTrue(Utils.CompareStrings("A s d", "A s d"));

            Assert.IsFalse(Utils.CompareStrings("asd", "Asd"));
            Assert.IsFalse(Utils.CompareStrings("A S D", "A s D"));
        }
    }
}
