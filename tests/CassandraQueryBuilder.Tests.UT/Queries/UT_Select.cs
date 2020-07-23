using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CassandraQueryBuilder;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_Select
    {
        [TestMethod]
        public void UT_Select_GetString()
        {
            String result = "SELECT * FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .ToString()
                )
            ;

            result = "SELECT v1 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1)
                    .ToString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .ToString()
                )
            ;

            result = "SELECT v1 FROM ks.tb WHERE v2 = ?;";
            Assert.AreEqual(result,
                new Select()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1)
                    .SetWhereColumns(Columns.columns2)
                    .ToString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v3 = ?;";
            Assert.AreEqual(result,
                new Select()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetWhereColumns(Columns.columns3)
                    .ToString()
                )
            ;

            result = "SELECT v1 FROM ks.tb WHERE v2 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Select()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1)
                    .SetWhereColumns(Columns.columns2, Columns.columns3)
                    .ToString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Select()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetWhereColumns(Columns.columns1, Columns.columns3)
                    .ToString()
                )
            ;

            //--- LIMIT
            
            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ? LIMIT 1;";
            Assert.AreEqual(result,
                new Select()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetWhereColumns(Columns.columns1, Columns.columns3)
                    .SetLimit(1)
                    .ToString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ? LIMIT ?;";
            Assert.AreEqual(result,
                new Select()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetWhereColumns(Columns.columns1, Columns.columns3)
                    .SetLimit()
                    .ToString()
                )
            ;

            //--- IN

            result = "SELECT v1 FROM ks.tb WHERE v1 IN (?);";
            Assert.AreEqual(result,
                new Select()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1)
                    .SetInColumns(Columns.columns1, 1)
                    .ToString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 IN (?);";
            Assert.AreEqual(result,
                new Select()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetInColumns(Columns.columns1, 1)
                    .ToString()
                )
            ;

            result = "SELECT v1 FROM ks.tb WHERE v1 IN (?, ?);";
            Assert.AreEqual(result,
                new Select()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1)
                    .SetInColumns(Columns.columns1, 2)
                    .ToString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 IN (?, ?);";
            Assert.AreEqual(result,
                new Select()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetInColumns(Columns.columns1, 2)
                    .ToString()
                )
            ;

            //--- "AND" AND IN AND LIMIT


            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ? AND v1 IN (?, ?) LIMIT 1;";
            Assert.AreEqual(result,
                new Select()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetWhereColumns(Columns.columns1, Columns.columns3)
                    .SetInColumns(Columns.columns1, 2)
                    .SetLimit(1)
                    .ToString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v2 < ? AND v3 > ? AND v1 IN (?, ?) LIMIT 1;";
            Assert.AreEqual(result,
                new Select()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .SetWhereColumns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .SetWhereSigns("=", "<", ">")
                    .SetInColumns(Columns.columns1, 2)
                    .SetLimit(1)
                    .ToString()
                )
            ;
        }

        [TestMethod]
        public void UT_Select_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new Select()
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new Select()
                        .SetKeyspace(Variables.keyspace)
                        .ToString();
                }
            );

            Assert.ThrowsException<IndexOutOfRangeException>(
                () => {
                    new Select()
                        .SetKeyspace(Variables.keyspace).SetTableName(Tables.tableName)
                        .SetWhereColumns(Columns.columns1, Columns.columns2)
                        .SetWhereSigns("=")
                        .ToString();
                }
            );
        }


    }
}
