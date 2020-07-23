using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CassandraQueryBuilder;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_UpdateCounter
    {
        [TestMethod]
        public void UT_UpdateCounter_GetString()
        {
            String result = "UPDATE ks.tb SET counter_column_name = counter_column_name + 1 WHERE pk1 = ?;";
            Assert.AreEqual(result,
                new UpdateCounter()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columnsCounter)
                    .SetWhereVariables(Columns.partitionKey1)
                    .SetIncreaseBy(1)
                    .ToString()
                )
            ;

            result = "UPDATE ks.tb SET counter_column_name = counter_column_name + 1 WHERE pk1 = ? AND pk2 = ?;";
            Assert.AreEqual(result,
                new UpdateCounter()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columnsCounter)
                    .SetWhereVariables(Columns.partitionKey1, Columns.partitionKey2)
                    .SetIncreaseBy(1)
                    .ToString()
                )
            ;

            result = "UPDATE ks.tb SET counter_column_name = counter_column_name + -1 WHERE pk1 = ? AND pk2 = ?;";
            Assert.AreEqual(result,
                new UpdateCounter()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columnsCounter)
                    .SetWhereVariables(Columns.partitionKey1, Columns.partitionKey2)
                    .SetIncreaseBy(-1)
                    .ToString()
                )
            ;

            result = "UPDATE ks.tb SET counter_column_name = counter_column_name + ? WHERE pk1 = ? AND pk2 = ?;";
            Assert.AreEqual(result,
                new UpdateCounter()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columnsCounter)
                    .SetWhereVariables(Columns.partitionKey1, Columns.partitionKey2)
                    .ToString()
                )
            ;

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
                        .SetKeyspace(Variables.keyspace)
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new UpdateCounter()
                        .SetKeyspace(Variables.keyspace)
                        .SetTableName(Tables.tableName)
                        .ToString();
                })
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new UpdateCounter()
                        .SetKeyspace(Variables.keyspace)
                        .SetTableName(Tables.tableName)
                        .SetVariables(Columns.columns1)
                        .ToString();
                })
            );
        }


    }
}
