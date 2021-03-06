﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_Select
    {
        [TestMethod]
        public void UT_Select_ToString()
        {
            String result = "SELECT * FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .ToString()
            );

            result = "SELECT v1 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1)
                    .ToString()
            );

            result = "SELECT v1, v2 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .ToString()
            );

            result = "SELECT v1 FROM ks.tb WHERE v2 = ?;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1)
                    .WhereColumns(Columns.columns2)
                    .ToString()
            );

            result = "SELECT v1, v2 FROM ks.tb WHERE v3 = ?;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns3)
                    .ToString()
            );

            result = "SELECT v1 FROM ks.tb WHERE v2 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1)
                    .WhereColumns(Columns.columns2, Columns.columns3)
                    .ToString()
            );

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .ToString()
            );

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
            );

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ? LIMIT ?;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .Limit()
                    .ToString()
            );

            //--- IN

            result = "SELECT v1 FROM ks.tb WHERE v2 IN (?);";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1)
                    .WhereColumns(Columns.columns2)
                    .InColumns(1)
                    .ToString()
            );

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 IN (?);";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1)
                    .InColumns(1)
                    .ToString()
            );

            result = "SELECT v1 FROM ks.tb WHERE v1 IN (?, ?);";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1)
                    .WhereColumns(Columns.columns1)
                    .InColumns(2)
                    .ToString()
            );

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 IN (?, ?);";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1)
                    .InColumns(2)
                    .ToString()
            );

            //--- "AND" AND IN AND LIMIT


            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ? AND v1 IN (?, ?) LIMIT 1;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3, Columns.columns1)
                    .InColumns(null, 0, 2) //null and 0 is same
                    .Limit(1)
                    .ToString()
            );

            result = "SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v2 < ? AND v3 > ? AND v1 IN (?, ?) LIMIT 1;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns2, Columns.columns3, Columns.columns1)
                    .WhereOperators(null, WhereOperator.SmallerThan, WhereOperator.LargerThan, null) // =, <, > (null is same as = or used for IN operator
                    .InColumns(null, null, null, 2)
                    .Limit(1)
                    .ToString()
            );
            
            result = "SELECT v1, v2 FROM ks.tb WHERE v1 IN (?, ?) AND v1 = ? AND v2 < ? AND v3 > ? LIMIT 1;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns1, Columns.columns2, Columns.columns3)
                    .WhereOperators(null, null, WhereOperator.SmallerThan, WhereOperator.LargerThan) // =, <, > (null is same as = or used for IN operator
                    .InColumns(2, null, null, null)
                    .Limit(1)
                    .ToString()
            );
        }


        [TestMethod]
        public void UT_Select_AS_ToString()
        {
            String result = "SELECT v1 AS v3, v2 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectAs(Columns.columns3)
                    .ToString()
            );

            result = "SELECT v1, v2 AS v3 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectAs(null, Columns.columns3)
                    .ToString()
            );

            result = "SELECT v1 AS v3, v2 AS v4 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectAs(Columns.columns3, Columns.columns4)
                    .ToString()
            );
        }

        [TestMethod]
        public void UT_Select_Functions_ToString()
        {
            String result = "SELECT TTL(v1) FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1)
                    .SelectFunctions(SelectFunction.TTL)
                    .ToString()
            );

            result = "SELECT TTL(v1), TTL(v2) FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectFunctions(SelectFunction.TTL, SelectFunction.TTL)
                    .ToString()
            );

            result = "SELECT v1, TTL(v2) FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectFunctions(null, SelectFunction.TTL)
                    .ToString()
            );

            result = "SELECT TTL(v1), v2 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectFunctions(SelectFunction.TTL, null)
                    .ToString()
            );

            result = "SELECT TTL(v1), v2 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectFunctions(SelectFunction.TTL)
                    .ToString()
            );

        }

        [TestMethod]
        public void UT_Select_Aggregates_ToString()
        {
            String result = "SELECT COUNT(*) FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectAggregates(SelectAggregate.COUNT)
                    .ToString()
            );

            result = "SELECT COUNT(v1) FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1)
                    .SelectAggregates(SelectAggregate.COUNT)
                    .ToString()
            );

            result = "SELECT AVG(v1), MIN(v2) FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectAggregates(SelectAggregate.AVG, SelectAggregate.MIN)
                    .ToString()
            );

            result = "SELECT v1, MIN(v2) FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectAggregates(null, SelectAggregate.MIN)
                    .ToString()
            );

            result = "SELECT AVG(v1), v2 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectAggregates(SelectAggregate.AVG, null)
                    .ToString()
            );

            result = "SELECT AVG(v1), v2 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectAggregates(SelectAggregate.AVG)
                    .ToString()
            );

        }


        [TestMethod]
        public void UT_Select_AS_and_Function_ToString()
        {
            String result = "SELECT TTL(v1) AS ttl FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1)
                    .SelectAs(new Column("ttl"))
                    .SelectFunctions(SelectFunction.TTL)
                    .ToString()
            );

            result = "SELECT v1, TTL(v2) AS ttl FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectAs(null, new Column("ttl"))
                    .SelectFunctions(null, SelectFunction.TTL)
                    .ToString()
            );

            result = "SELECT TTL(v1) AS ttl, v2 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectAs(new Column("ttl"), null)
                    .SelectFunctions(SelectFunction.TTL, null)
                    .ToString()
            );

            result = "SELECT TTL(v1) AS ttl, v2 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectAs(new Column("ttl"))
                    .SelectFunctions(SelectFunction.TTL, null)
                    .ToString()
            );

            result = "SELECT TTL(v1) AS ttl, v2 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectAs(new Column("ttl"), null)
                    .SelectFunctions(SelectFunction.TTL)
                    .ToString()
            );
            
            result = "SELECT TTL(v1) AS ttl, v2 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectAs(new Column("ttl"))
                    .SelectFunctions(SelectFunction.TTL)
                    .ToString()
            );
        }

        [TestMethod]
        public void UT_Select_Function_and_Aggregates_ToString()
        {
            String result = "SELECT TTL(v1), AVG(v2) FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectFunctions(SelectFunction.TTL, null)
                    .SelectAggregates(null, SelectAggregate.AVG)
                    .ToString()
            );

            result = "SELECT MIN(v1), TTL(v2) FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2)
                    .SelectFunctions(null, SelectFunction.TTL)
                    .SelectAggregates(SelectAggregate.MIN, null)
                    .ToString()
            );

            result = "SELECT AVG(v1), TTL(v2), v3 FROM ks.tb;";
            Assert.AreEqual(result,
                new Select()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .SelectColumns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .SelectFunctions(null, SelectFunction.TTL, null)
                    .SelectAggregates(SelectAggregate.AVG, null, null)
                    .ToString()
            );

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
                        .SelectColumns(Columns.columns1)
                        .SelectAs(Columns.columns2, Columns.columns3)
                        .ToString();
                }
            );

            Assert.ThrowsException<IndexOutOfRangeException>(
                () => {
                    new Select()
                        .Keyspace(Variables.keyspace).Table(Tables.tableName)
                        .SelectColumns(Columns.columns1)
                        .SelectFunctions(SelectFunction.TTL, SelectFunction.TTL)
                        .ToString();
                }
            );

            Assert.ThrowsException<IndexOutOfRangeException>(
                () => {
                    new Select()
                        .Keyspace(Variables.keyspace).Table(Tables.tableName)
                        .SelectColumns(Columns.columns1)
                        .SelectAggregates(SelectAggregate.COUNT, SelectAggregate.MAX)
                        .ToString();
                }
            );
            
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => {
                    new Select()
                        .Keyspace(Variables.keyspace).Table(Tables.tableName)
                        .WhereColumns(Columns.columns1, Columns.columns2)
                        .WhereOperators(WhereOperator.EqualTo)
                        .ToString();
                }
            );
        }


    }
}
