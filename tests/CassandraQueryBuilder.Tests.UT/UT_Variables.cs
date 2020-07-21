using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DB.CassandraQueryBuilder;

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
