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
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .ToString()
                )
            ;

            result = "SELECT v1 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1)
                    .ToString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .ToString()
                )
            ;

            result = "SELECT v1 FROM ks.tb WHERE v2 = ?;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1)
                    .WhereColumns(Columns.columns2)
                    .ToString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v3 = ?;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns3)
                    .ToString()
                )
            ;

            result = "SELECT v1 FROM ks.tb WHERE v2 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1)
                    .WhereColumns(Columns.columns2, Columns.columns3)
                    .ToString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .ToString()
                )
            ;

            //--- LIMIT
            
            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ? LIMIT 1;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .Limit(1)
                    .ToString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ? LIMIT ?;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .Limit()
                    .ToString()
                )
            ;

            //--- IN

            result = "SELECT v1 FROM ks.tb WHERE v1 IN (?);";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1)
                    .InColumns(Columns.columns1, 1)
                    .ToString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 IN (?);";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .InColumns(Columns.columns1, 1)
                    .ToString()
                )
            ;

            result = "SELECT v1 FROM ks.tb WHERE v1 IN (?, ?);";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1)
                    .InColumns(Columns.columns1, 2)
                    .ToString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 IN (?, ?);";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .InColumns(Columns.columns1, 2)
                    .ToString()
                )
            ;

            //--- "AND" AND IN AND LIMIT


            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ? AND v1 IN (?, ?) LIMIT 1;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .InColumns(Columns.columns1, 2)
                    .Limit(1)
                    .ToString()
                )
            ;

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v2 < ? AND v3 > ? AND v1 IN (?, ?) LIMIT 1;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .WhereSigns("=", "<", ">")
                    .InColumns(Columns.columns1, 2)
                    .Limit(1)
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
                        .Keyspace(Variables.keyspace)
                        .ToString();
                }
            );

            Assert.ThrowsException<IndexOutOfRangeException>(
                () => {
                    new Select()
                        .Keyspace(Variables.keyspace).Table(Tables.tableName)
                        .WhereColumns(Columns.columns1, Columns.columns2)
                        .WhereSigns("=")
                        .ToString();
                }
            );
        }


    }
}
