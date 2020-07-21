using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DB.CassandraQueryBuilder;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_InsertQuery
    {
        [TestMethod]
        public void UT_InsertQuery_GetString()
        {
            String result = "INSERT INTO ks.tb (v1) VALUES (?);";
            Assert.AreEqual(result,
                new InsertQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1)
                    .GetString()
                )
            ;

            result = "INSERT INTO ks.tb (v1) VALUES (?) IF NOT EXISTS;";
            Assert.AreEqual(result,
                new InsertQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1)
                    .SetIfNotExists()
                    .GetString()
                )
            ;

            result = "INSERT INTO ks.tb (v1, v2) VALUES (?, ?);";
            Assert.AreEqual(result,
                new InsertQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .GetString()
                )
            ;

            result = "INSERT INTO ks.tb (v1, v2) VALUES (?, ?) IF NOT EXISTS;";
            Assert.AreEqual(result,
                new InsertQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetIfNotExists()
                    .GetString()
                )
            ;

            result = "INSERT INTO ks.tb (v1, v2) VALUES (?, ?) IF NOT EXISTS USING TTL ?;";
            Assert.AreEqual(result,
                new InsertQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetTTL()
                    .SetIfNotExists()
                    .GetString()
                )
            ;

            result = "INSERT INTO ks.tb (v1, v2) VALUES (?, ?) IF NOT EXISTS USING TIMESTAMP ?;";
            Assert.AreEqual(result,
                new InsertQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetIfNotExists()
                    .SetTimestamp()
                    .GetString()
                )
            ;

            result = "INSERT INTO ks.tb (v1, v2) VALUES (?, ?) IF NOT EXISTS USING TIMESTAMP ? AND TTL ?;";
            Assert.AreEqual(result,
                new InsertQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetTTL()
                    .SetIfNotExists()
                    .SetTimestamp()
                    .GetString()
                )
            ;
        }

        [TestMethod]
        public void UT_InsertQuery_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new InsertQuery()
                        .GetString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new InsertQuery()
                        .SetKeyspace(Variables.keyspace)
                        .GetString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new InsertQuery()
                        .SetKeyspace(Variables.keyspace)
                        .SetTableName(Tables.tableName)
                        .GetString();
                })
            );
        }


    }
}
