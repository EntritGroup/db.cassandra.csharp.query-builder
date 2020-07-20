using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DB.Cassandra.QueryBuilder;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_UpdateQuery
    {
        [TestMethod]
        public void UT_UpdateQuery_GetString()
        {
            String result = "UPDATE ks.tb SET v1 = ? WHERE v2 = ?;";
            Assert.AreEqual(result,
                new UpdateQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1)
                    .SetWhereVariables(Columns.columns2)
                    .GetString()
                )
            ;
            result = "UPDATE ks.tb SET v1 = ? WHERE v2 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new UpdateQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1)
                    .SetWhereVariables(Columns.columns2)
                    .SetIfExists()
                    .GetString()
                )
            ;
            
            result = "UPDATE ks.tb SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new UpdateQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .GetString()
                )
            ;

            result = "UPDATE ks.tb SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new UpdateQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetIfExists()
                    .GetString()
                )
            ;

            result = "UPDATE ks.tb USING TTL ? SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new UpdateQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetIfExists()
                    .SetTTL()
                    .GetString()
                )
            ;

            result = "UPDATE ks.tb USING TIMESTAMP ? SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new UpdateQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetIfExists()
                    .SetTimestamp()
                    .GetString()
                )
            ;

            result = "UPDATE ks.tb USING TIMESTAMP ? AND TTL ? SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new UpdateQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetIfExists()
                    .SetTimestamp()
                    .SetTTL()
                    .GetString()
                )
            ;

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vl1 = ? + vl1 WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new UpdateQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2, Columns.columns_list1)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetListUpdateType(ListUpdateType.PREPEND)
                    .GetString()
                )
            ;

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vl1 = vl1 + ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new UpdateQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2, Columns.columns_list1)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetListUpdateType(ListUpdateType.APPEND)
                    .GetString()
                )
            ;

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vl1 = ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new UpdateQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2, Columns.columns_list1)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetListUpdateType(ListUpdateType.REPLACE_ALL)
                    .GetString()
                )
            ;

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vl1[?] = ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new UpdateQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2, Columns.columns_list1)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetListUpdateType(ListUpdateType.SPECIFY_INDEX_TO_OVERWRITE)
                    .GetString()
                )
            ;
        }

        [TestMethod]
        public void UT_UpdateQuery_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new UpdateQuery()
                        .GetString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new UpdateQuery()
                        .SetKeyspace(Variables.keyspace)
                        .GetString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new UpdateQuery()
                        .SetKeyspace(Variables.keyspace)
                        .SetTableName(Tables.tableName)
                        .GetString();
                })
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new UpdateQuery()
                        .SetKeyspace(Variables.keyspace)
                        .SetTableName(Tables.tableName)
                        .SetVariables(Columns.columns1)
                        .GetString();
                })
            );
        }


    }
}
