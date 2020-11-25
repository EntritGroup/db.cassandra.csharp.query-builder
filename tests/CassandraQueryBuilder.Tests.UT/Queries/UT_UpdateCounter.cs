using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_UpdateCounter
    {
        [TestMethod]
        public void UT_UpdateCounter_GetString()
        {
            String result = "UPDATE ks.tb SET c1 = c1 + 1 WHERE pk1 = ?;";
            Assert.AreEqual(result,
                new UpdateCounter()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumn(Columns.columnsCounter1)
                    .WhereColumns(Columns.partitionKey1)
                    .IncreaseBy(1)
                    .ToString()
            );

            result = "UPDATE ks.tb SET c1 = c1 + 1 WHERE pk1 = ? AND pk2 = ?;";
            Assert.AreEqual(result,
                new UpdateCounter()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumn(Columns.columnsCounter1)
                    .WhereColumns(Columns.partitionKey1, Columns.partitionKey2)
                    .IncreaseBy(1)
                    .ToString()
            );

            result = "UPDATE ks.tb SET c1 = c1 + -1 WHERE pk1 = ? AND pk2 = ?;";
            Assert.AreEqual(result,
                new UpdateCounter()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumn(Columns.columnsCounter1)
                    .WhereColumns(Columns.partitionKey1, Columns.partitionKey2)
                    .IncreaseBy(-1)
                    .ToString()
            );

            result = "UPDATE ks.tb SET c1 = c1 + ? WHERE pk1 = ? AND pk2 = ?;";
            Assert.AreEqual(result,
                new UpdateCounter()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumn(Columns.columnsCounter1)
                    .WhereColumns(Columns.partitionKey1, Columns.partitionKey2)
                    .ToString()
            );

        }

        [TestMethod]
        public void UT_UpdateCounter_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new UpdateCounter()
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new UpdateCounter()
                        .Keyspace(Variables.keyspace)
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new UpdateCounter()
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .ToString();
                })
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new UpdateCounter()
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .UpdateColumn(Columns.columns1)
                        .ToString();
                })
            );
        }


    }
}
