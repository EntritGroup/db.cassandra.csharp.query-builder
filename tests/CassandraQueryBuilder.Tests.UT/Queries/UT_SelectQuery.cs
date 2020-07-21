using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DB.CassandraQueryBuilder;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_SelectQuery
    {
        [TestMethod]
        public void UT_SelectQuery_GetString()
        {
            String result = "SELECT * FROM ks.tb;";
            Assert.AreEqual(result,
                new SelectQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .GetString()
                )
            ;

            result = "SELECT v1 FROM ks.tb;";
            Assert.AreEqual(result,
                new SelectQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1)
                    .GetString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb;";
            Assert.AreEqual(result,
                new SelectQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .GetString()
                )
            ;

            result = "SELECT v1 FROM ks.tb WHERE v2 = ?;";
            Assert.AreEqual(result,
                new SelectQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1)
                    .SetWhereColumns(Columns.columns2)
                    .GetString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v3 = ?;";
            Assert.AreEqual(result,
                new SelectQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetWhereColumns(Columns.columns3)
                    .GetString()
                )
            ;

            result = "SELECT v1 FROM ks.tb WHERE v2 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new SelectQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1)
                    .SetWhereColumns(Columns.columns2, Columns.columns3)
                    .GetString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new SelectQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetWhereColumns(Columns.columns1, Columns.columns3)
                    .GetString()
                )
            ;

            //--- LIMIT
            
            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ? LIMIT 1;";
            Assert.AreEqual(result,
                new SelectQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetWhereColumns(Columns.columns1, Columns.columns3)
                    .SetLimit(1)
                    .GetString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ? LIMIT ?;";
            Assert.AreEqual(result,
                new SelectQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetWhereColumns(Columns.columns1, Columns.columns3)
                    .SetLimit()
                    .GetString()
                )
            ;

            //--- IN

            result = "SELECT v1 FROM ks.tb WHERE v1 IN (?);";
            Assert.AreEqual(result,
                new SelectQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1)
                    .SetInColumns(Columns.columns1, 1)
                    .GetString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 IN (?);";
            Assert.AreEqual(result,
                new SelectQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetInColumns(Columns.columns1, 1)
                    .GetString()
                )
            ;

            result = "SELECT v1 FROM ks.tb WHERE v1 IN (?, ?);";
            Assert.AreEqual(result,
                new SelectQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1)
                    .SetInColumns(Columns.columns1, 2)
                    .GetString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 IN (?, ?);";
            Assert.AreEqual(result,
                new SelectQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetInColumns(Columns.columns1, 2)
                    .GetString()
                )
            ;

            //--- "AND" AND IN AND LIMIT


            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ? AND v1 IN (?, ?) LIMIT 1;";
            Assert.AreEqual(result,
                new SelectQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetWhereColumns(Columns.columns1, Columns.columns3)
                    .SetInColumns(Columns.columns1, 2)
                    .SetLimit(1)
                    .GetString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v2 < ? AND v3 > ? AND v1 IN (?, ?) LIMIT 1;";
            Assert.AreEqual(result,
                new SelectQuery()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetWhereColumns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .SetWhereSigns("=", "<", ">")
                    .SetInColumns(Columns.columns1, 2)
                    .SetLimit(1)
                    .GetString()
                )
            ;
        }

        [TestMethod]
        public void UT_SelectQuery_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new SelectQuery()
                        .GetString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new SelectQuery()
                        .SetKeyspace(Variables.keyspace)
                        .GetString();
                }
            );

            Assert.ThrowsException<IndexOutOfRangeException>(
                () => {
                    new SelectQuery()
                        .SetKeyspace(Variables.keyspace).SetTableName(Tables.tableName)
                        .SetWhereColumns(Columns.columns1, Columns.columns2)
                        .SetWhereSigns("=")
                        .GetString();
                }
            );
        }


    }
}
