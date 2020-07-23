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
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1)
                    .SetWhereVariables(Columns.columns2)
                    .ToString()
                )
            ;
            result = "UPDATE ks.tb SET v1 = ? WHERE v2 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Update()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1)
                    .SetWhereVariables(Columns.columns2)
                    .SetIfExists()
                    .ToString()
                )
            ;
            
            result = "UPDATE ks.tb SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .ToString()
                )
            ;

            result = "UPDATE ks.tb SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Update()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetIfExists()
                    .ToString()
                )
            ;

            result = "UPDATE ks.tb USING TTL ? SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Update()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetIfExists()
                    .SetTTL()
                    .ToString()
                )
            ;

            result = "UPDATE ks.tb USING TIMESTAMP ? SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Update()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetIfExists()
                    .SetTimestamp()
                    .ToString()
                )
            ;

            result = "UPDATE ks.tb USING TIMESTAMP ? AND TTL ? SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Update()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetIfExists()
                    .SetTimestamp()
                    .SetTTL()
                    .ToString()
                )
            ;

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vl1 = ? + vl1 WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2, Columns.columns_list1)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetListUpdateType(ListUpdateType.PREPEND)
                    .ToString()
                )
            ;

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vl1 = vl1 + ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2, Columns.columns_list1)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetListUpdateType(ListUpdateType.APPEND)
                    .ToString()
                )
            ;

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vl1 = ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2, Columns.columns_list1)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetListUpdateType(ListUpdateType.REPLACE_ALL)
                    .ToString()
                )
            ;

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vl1[?] = ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2, Columns.columns_list1)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetListUpdateType(ListUpdateType.SPECIFY_INDEX_TO_OVERWRITE)
                    .ToString()
                )
            ;
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
                        .SetKeyspace(Variables.keyspace)
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new Update()
                        .SetKeyspace(Variables.keyspace)
                        .SetTableName(Tables.tableName)
                        .ToString();
                })
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new Update()
                        .SetKeyspace(Variables.keyspace)
                        .SetTableName(Tables.tableName)
                        .SetVariables(Columns.columns1)
                        .ToString();
                })
            );
        }


    }
}
