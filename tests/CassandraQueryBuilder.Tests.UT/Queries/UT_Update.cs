using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CassandraQueryBuilder;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_Update
    {
        [TestMethod]
        public void UT_Update_GetString()
        {
            String result = "UPDATE ks.tb SET v1 = ? WHERE v2 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1)
                    .WhereColumns(Columns.columns2)
                    .ToString()
            );

            result = "UPDATE ks.tb SET v1 = ? WHERE v2 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1)
                    .WhereColumns(Columns.columns2)
                    .IfExists()
                    .ToString()
            );
            
            result = "UPDATE ks.tb SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .ToString()
            );

            result = "UPDATE ks.tb SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .IfExists()
                    .ToString()
            );

            result = "UPDATE ks.tb USING TTL ? SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .IfExists()
                    .TTL()
                    .ToString()
            );

            result = "UPDATE ks.tb USING TIMESTAMP ? SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .IfExists()
                    .Timestamp()
                    .ToString()
            );

            result = "UPDATE ks.tb USING TIMESTAMP ? AND TTL ? SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .IfExists()
                    .Timestamp()
                    .TTL()
                    .ToString()
            );

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vl1 = ? + vl1 WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2, Columns.columns_list1)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .ListUpdateType(ListUpdateType.PREPEND)
                    .ToString()
            );

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vl1 = vl1 + ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2, Columns.columns_list1)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .ListUpdateType(ListUpdateType.APPEND)
                    .ToString()
            );

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vl1 = ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2, Columns.columns_list1)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .ListUpdateType(ListUpdateType.REPLACE_ALL)
                    .ToString()
            );

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vl1[?] = ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2, Columns.columns_list1)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .ListUpdateType(ListUpdateType.SPECIFY_INDEX_TO_OVERWRITE)
                    .ToString()
            );
        }

        [TestMethod]
        public void UT_Update_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new Update()
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new Update()
                        .Keyspace(Variables.keyspace)
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new Update()
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .ToString();
                })
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new Update()
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .UpdateColumns(Columns.columns1)
                        .ToString();
                })
            );
        }


    }
}
