using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_Types
    {

        
        [TestMethod]
        public void UT_Types_GetString()
        {
            String result = "CREATE TYPE ks.tb (v1 TEXT, v2 TEXT, v3 TEXT);";
            Assert.AreEqual(result,
                new CreateType()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .Columns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .ToString()
            );
            
            result = "CREATE TYPE ks.tb (v1 TEXT, vl1 LIST<TEXT>, v3 TEXT);";
            Assert.AreEqual(result,
                new CreateType()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .Columns(Columns.columns1, Columns.columns_list1, Columns.columns3)
                    .ToString()
            );

        }

        [TestMethod]
        public void UT_Tables_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateTable()
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateTable()
                        .Keyspace(Variables.keyspace)
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateTable()
                        .Table(Tables.tableName)
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateTable()
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .ToString();
                }
            );

        }


    }
}
