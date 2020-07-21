using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DB.CassandraQueryBuilder;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_DeleteQuery
    {
        [TestMethod]
        public void UT_DeleteQuery_GetString()
        {

            String result = "DELETE FROM ks.tb WHERE v1 = ?;";
            Assert.AreEqual(result,
                new DeleteQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetWhereVariables(Columns.columns1)
                    .GetString()
                )
            ;
            result = "DELETE FROM ks.tb WHERE v1 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new DeleteQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetWhereVariables(Columns.columns1)
                    .SetIfExists()
                    .GetString()
                )
            ;

            result = "DELETE v1 FROM ks.tb WHERE v2 = ?;";
            Assert.AreEqual(result,
                new DeleteQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1)
                    .SetWhereVariables(Columns.columns2)
                    .GetString()
                )
            ;
            result = "DELETE v1 FROM ks.tb WHERE v2 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new DeleteQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1)
                    .SetWhereVariables(Columns.columns2)
                    .SetIfExists()
                    .GetString()
                )
            ;

            result = "DELETE v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new DeleteQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .GetString()
                )
            ;
            result = "DELETE v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new DeleteQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetIfExists()
                    .GetString()
                )
            ;
            
            result = "DELETE v1, v2 FROM ks.tb USING TIMESTAMP ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new DeleteQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetIfExists()
                    .SetTimestamp()
                    .GetString()
                )
            ;

            result = "DELETE vl1[?], v2 FROM ks.tb WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new DeleteQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns_list1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetListDeleteType(ListDeleteType.SELECTED)
                    .GetString()
                )
            ;

            result = "DELETE vl1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new DeleteQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns_list1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetListDeleteType(ListDeleteType.ALL)
                    .GetString()
                )
            ;
        }

        [TestMethod]
        public void UT_DeleteQuery_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new DeleteQuery()
                        .GetString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new DeleteQuery()
                        .SetKeyspace(Variables.keyspace)
                        .GetString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new DeleteQuery()
                        .SetKeyspace(Variables.keyspace)
                        .SetTableName(Tables.tableName)
                        .GetString();
                })
            );
        }


    }
}
