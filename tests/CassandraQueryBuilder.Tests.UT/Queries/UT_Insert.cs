using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_Insert
    {
        [TestMethod]
        public void UT_Insert_GetString()
        {
            String result = "INSERT INTO ks.tb (v1) VALUES (?);";
            Assert.AreEqual(result,
                new Insert()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .InsertColumns(Columns.columns1)
                    .ToString()
            );

            result = "INSERT INTO ks.tb (v1) VALUES (?) IF NOT EXISTS;";
            Assert.AreEqual(result,
                new Insert()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .InsertColumns(Columns.columns1)
                    .IfNotExists()
                    .ToString()
            );

            result = "INSERT INTO ks.tb (v1, v2) VALUES (?, ?);";
            Assert.AreEqual(result,
                new Insert()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .InsertColumns(Columns.columns1, Columns.columns2)
                    .ToString()
            );

            result = "INSERT INTO ks.tb (v1, v2) VALUES (?, ?) IF NOT EXISTS;";
            Assert.AreEqual(result,
                new Insert()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .InsertColumns(Columns.columns1, Columns.columns2)
                    .IfNotExists()
                    .ToString()
            );

            result = "INSERT INTO ks.tb (v1, v2) VALUES (?, ?) IF NOT EXISTS USING TTL ?;";
            Assert.AreEqual(result,
                new Insert()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .InsertColumns(Columns.columns1, Columns.columns2)
                    .TTL()
                    .IfNotExists()
                    .ToString()
            );

            result = "INSERT INTO ks.tb (v1, v2) VALUES (?, ?) IF NOT EXISTS USING TIMESTAMP ?;";
            Assert.AreEqual(result,
                new Insert()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .InsertColumns(Columns.columns1, Columns.columns2)
                    .IfNotExists()
                    .Timestamp()
                    .ToString()
            );

            result = "INSERT INTO ks.tb (v1, v2) VALUES (?, ?) IF NOT EXISTS USING TIMESTAMP ? AND TTL ?;";
            Assert.AreEqual(result,
                new Insert()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .InsertColumns(Columns.columns1, Columns.columns2)
                    .TTL()
                    .IfNotExists()
                    .Timestamp()
                    .ToString()
            );
        }

        [TestMethod]
        public void UT_Insert_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new Insert()
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new Insert()
                        .Keyspace(Variables.keyspace)
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new Insert()
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .ToString();
                })
            );
        }


    }
}
