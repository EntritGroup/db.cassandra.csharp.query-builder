using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DB.Cassandra.QueryBuilder;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_UpdateCounterQuery
    {
        [TestMethod]
        public void UT_UpdateCounterQuery_GetString()
        {
            String result = "UPDATE ks.tb SET counter_column_name = counter_column_name + 1 WHERE pk1 = ?;";
            Assert.AreEqual(result,
                new UpdateCounterQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columnsCounter)
                    .SetWhereVariables(Columns.partitionKey1)
                    .SetIncreaseBy(1)
                    .GetString()
                )
            ;

            result = "UPDATE ks.tb SET counter_column_name = counter_column_name + 1 WHERE pk1 = ? AND pk2 = ?;";
            Assert.AreEqual(result,
                new UpdateCounterQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columnsCounter)
                    .SetWhereVariables(Columns.partitionKey1, Columns.partitionKey2)
                    .SetIncreaseBy(1)
                    .GetString()
                )
            ;

            result = "UPDATE ks.tb SET counter_column_name = counter_column_name + -1 WHERE pk1 = ? AND pk2 = ?;";
            Assert.AreEqual(result,
                new UpdateCounterQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columnsCounter)
                    .SetWhereVariables(Columns.partitionKey1, Columns.partitionKey2)
                    .SetIncreaseBy(-1)
                    .GetString()
                )
            ;

            result = "UPDATE ks.tb SET counter_column_name = counter_column_name + ? WHERE pk1 = ? AND pk2 = ?;";
            Assert.AreEqual(result,
                new UpdateCounterQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columnsCounter)
                    .SetWhereVariables(Columns.partitionKey1, Columns.partitionKey2)
                    .GetString()
                )
            ;

        }

        [TestMethod]
        public void UT_UpdateCounterQuery_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new UpdateCounterQuery()
                        .GetString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new UpdateCounterQuery()
                        .SetKeyspace(Variables.keyspace)
                        .GetString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new UpdateCounterQuery()
                        .SetKeyspace(Variables.keyspace)
                        .SetTableName(Tables.tableName)
                        .GetString();
                })
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new UpdateCounterQuery()
                        .SetKeyspace(Variables.keyspace)
                        .SetTableName(Tables.tableName)
                        .SetVariables(Columns.columns1)
                        .GetString();
                })
            );
        }


    }
}
