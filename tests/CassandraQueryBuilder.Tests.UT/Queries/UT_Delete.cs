using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CassandraQueryBuilder;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_Delete
    {
        [TestMethod]
        public void UT_Delete_GetString()
        {

            String result = "DELETE FROM ks.tb WHERE v1 = ?;";
            Assert.AreEqual(result,
                new Delete()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .WhereColumns(Columns.columns1)
                    .ToString()
            );

            result = "DELETE FROM ks.tb WHERE v1 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Delete()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .WhereColumns(Columns.columns1)
                    .IfExists()
                    .ToString()
            );

            result = "DELETE v1 FROM ks.tb WHERE v2 = ?;";
            Assert.AreEqual(result,
                new Delete()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .DeleteColumns(Columns.columns1)
                    .WhereColumns(Columns.columns2)
                    .ToString()
            );

            result = "DELETE v1 FROM ks.tb WHERE v2 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Delete()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .DeleteColumns(Columns.columns1)
                    .WhereColumns(Columns.columns2)
                    .IfExists()
                    .ToString()
            );

            result = "DELETE v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Delete()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .DeleteColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .ToString()
            );

            result = "DELETE v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Delete()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .DeleteColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .IfExists()
                    .ToString()
            );
            
            result = "DELETE v1, v2 FROM ks.tb USING TIMESTAMP ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Delete()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .DeleteColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .IfExists()
                    .Timestamp()
                    .ToString()
            );

            result = "DELETE vl1[?], v2 FROM ks.tb WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Delete()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .DeleteColumns(Columns.columns_list1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .ListDeleteType(ListDeleteType.SELECTED)
                    .ToString()
            );

            result = "DELETE vl1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Delete()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .DeleteColumns(Columns.columns_list1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .ListDeleteType(ListDeleteType.ALL)
                    .ToString()
            );
        }

        [TestMethod]
        public void UT_Delete_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new Delete()
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new Delete()
                        .Keyspace(Variables.keyspace)
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new Delete()
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .ToString();
                })
            );
        }


    }
}
