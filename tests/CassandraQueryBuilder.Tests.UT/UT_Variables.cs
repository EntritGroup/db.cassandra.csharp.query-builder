using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_Variables
    {
        private static readonly Column variable = new Column ("v1", ColumnType.TEXT, true);

        [TestMethod]
        public void UT_Utils_GetVariablesName()
        {
            Assert.AreEqual("v1", variable.GetName());
        }

        [TestMethod]
        public void UT_Utils_GetVariablesType()
        {
            Assert.AreEqual("TEXT", variable.GetColumnType());
        }

        [TestMethod]
        public void UT_Utils_GetVariablesStatic()
        {
            Assert.IsTrue(variable.IsStatic());
        }
    }
}
