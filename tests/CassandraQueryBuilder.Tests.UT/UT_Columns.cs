using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_Columns
    {

        [TestMethod]
        public void UT_Columns_GetColumnName()
        {
            Column column = new Column ("v1", ColumnType.TEXT, true);
            Assert.AreEqual("v1", column.Name());
        }

        [TestMethod]
        public void UT_Columns_GetColumnType()
        {
            Column column = new Column ("v1", ColumnType.TEXT, true);
            Assert.AreEqual("TEXT", column.ColumnType());
        }

        [TestMethod]
        public void UT_Columns_GetColumnStatic()
        {
            Column column = new Column ("v1", ColumnType.TEXT, true);
            Assert.IsTrue(column.IsStatic());
        }
        
        [TestMethod]
        public void UT_ColumnTypes_GetFrozenColumn()
        {
            Column column = new Column ("l1", ColumnType.FROZEN(ColumnType.LIST(ColumnType.TEXT)));
            Assert.AreEqual("FROZEN<LIST<TEXT>>", column.ColumnType());
        }

        [TestMethod]
        public void UT_ColumnTypes_GetTupleColumn()
        {
            Column column = new Column ("v1", ColumnType.TUPLE(new ColumnType[] { ColumnType.TEXT, ColumnType.BOOLEAN } ));
            Assert.AreEqual("TUPLE<TEXT, BOOLEAN>", column.ColumnType());
        }
    
        [TestMethod]
        public void UT_ColumnTypes_GetFrozenTupleColumn()
        {
            Column column = new Column ("v1", ColumnType.FROZEN(ColumnType.TUPLE(new ColumnType[] { ColumnType.TEXT, ColumnType.BOOLEAN } )));
            Assert.AreEqual("FROZEN<TUPLE<TEXT, BOOLEAN>>", column.ColumnType());
        }

        
        [TestMethod]
        public void UT_Columns_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    (new Column (null)).Name();
                }
            );
            
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    (new Column ("v1", null)).ColumnType();
                }
            );
        }
    }
}
