using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_Columns
    {
        private static readonly Column column = new Column ("v1", ColumnType.TEXT, true);

        [TestMethod]
        public void UT_Utils_GetColumnsName()
        {
            Assert.AreEqual("v1", column.Name());
        }

        [TestMethod]
        public void UT_Utils_GetColumnsType()
        {
            Assert.AreEqual("TEXT", column.ColumnType());
        }

        [TestMethod]
        public void UT_Utils_GetColumnsStatic()
        {
            Assert.IsTrue(column.IsStatic());
        }
    }
}
