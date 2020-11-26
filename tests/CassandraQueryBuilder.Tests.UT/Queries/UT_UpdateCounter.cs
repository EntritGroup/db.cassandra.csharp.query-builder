using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_UpdateCounter
    {
        [TestMethod]
        public void UT_UpdateCounters_GetString()
        {
            String result = "UPDATE ks.tb SET c1 = c1 + 1 WHERE pk1 = ?;";
            Assert.AreEqual(result,
                new UpdateCounter()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columnsCounter1)
                    .WhereColumns(Columns.partitionKey1)
                    .IncreaseBy(1)
                    .ToString()
            );

            result = "UPDATE ks.tb SET c1 = c1 + 1 WHERE pk1 = ? AND pk2 = ?;";
            Assert.AreEqual(result,
                new UpdateCounter()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columnsCounter1)
                    .WhereColumns(Columns.partitionKey1, Columns.partitionKey2)
                    .IncreaseBy(1)
                    .ToString()
            );

            result = "UPDATE ks.tb SET c1 = c1 + -1 WHERE pk1 = ? AND pk2 = ?;";
            Assert.AreEqual(result,
                new UpdateCounter()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columnsCounter1)
                    .WhereColumns(Columns.partitionKey1, Columns.partitionKey2)
                    .IncreaseBy(-1)
                    .ToString()
            );

            result = "UPDATE ks.tb SET c1 = c1 + ? WHERE pk1 = ? AND pk2 = ?;";
            Assert.AreEqual(result,
                new UpdateCounter()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columnsCounter1)
                    .WhereColumns(Columns.partitionKey1, Columns.partitionKey2)
                    .ToString()
            );

            result = "UPDATE ks.tb SET c1 = c1 + ?, c2 = c2 + ? WHERE pk1 = ? AND pk2 = ?;";
            Assert.AreEqual(result,
                new UpdateCounter()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columnsCounter1, Columns.columnsCounter2)
                    .WhereColumns(Columns.partitionKey1, Columns.partitionKey2)
                    .ToString()
            );

            result = "UPDATE ks.tb SET c1 = c1 + 1, c2 = c2 + ? WHERE pk1 = ? AND pk2 = ?;";
            Assert.AreEqual(result,
                new UpdateCounter()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columnsCounter1, Columns.columnsCounter2)
                    .WhereColumns(Columns.partitionKey1, Columns.partitionKey2)
                    .IncreaseBy(1, null)
                    .ToString()
            );

            result = "UPDATE ks.tb SET c1 = c1 + ?, c2 = c2 + -1 WHERE pk1 = ? AND pk2 = ?;";
            Assert.AreEqual(result,
                new UpdateCounter()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columnsCounter1, Columns.columnsCounter2)
                    .WhereColumns(Columns.partitionKey1, Columns.partitionKey2)
                    .IncreaseBy(null, -1)
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
                () => {
                    new UpdateCounter()
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new UpdateCounter()
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .UpdateColumns(Columns.columns1)
                        .ToString();
                }
            );
            
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new UpdateCounter()
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .UpdateColumns(Columns.columns1, Columns.columns2)
                        .IncreaseBy(1)
                        .ToString();
                }
            );
        }


    }
}
