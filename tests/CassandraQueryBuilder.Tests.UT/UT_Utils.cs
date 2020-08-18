using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        [TestMethod]
        public void UT_Utils_AppendColumnRow_Name()
        {
            StringBuilder sb = new StringBuilder();
            Column c = new Column("name", ColumnType.TEXT);
            
            Utils.AppendColumnRow(sb, c);
            
            String result = "name";
            Assert.AreEqual(result, sb.ToString());
        }

        [TestMethod]
        public void UT_Utils_AppendColumnRow_NameSuffix()
        {
            StringBuilder sb = new StringBuilder();
            Column c = new Column("name", ColumnType.TEXT);
            
            Utils.AppendColumnRow(sb, c, "suffix");
            
            String result = "namesuffix";
            Assert.AreEqual(result, sb.ToString());
        }
        
        [TestMethod]
        public void UT_Utils_AppendColumnRow_PrefixNameSuffix()
        {
            StringBuilder sb = new StringBuilder();
            Column c = new Column("name", ColumnType.TEXT);
            
            Utils.AppendColumnRow(sb, c, "prefix", "suffix");
            
            String result = "prefixnamesuffix";
            Assert.AreEqual(result, sb.ToString());
        }

        
        [TestMethod]
        public void UT_Utils_AppendColumnRows_Name()
        {
            StringBuilder sb = new StringBuilder();
            Column[] c = new Column[] {
                new Column ("name1", ColumnType.TEXT),
                new Column("name2", ColumnType.TIMEUUID)
            };
            
            Utils.AppendColumnRows(sb, c);
            
            String result = "name1, name2";
            Assert.AreEqual(result, sb.ToString());
        }
    }

}
