﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CassandraQueryBuilder;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_Delete
    {
        [TestMethod]
        public void UT_Delete_GetString()
        {

            String result = "DELETE FROM ks.tb WHERE v1 = ?;";
            Assert.AreEqual(result,
                new Delete()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetWhereVariables(Columns.columns1)
                    .ToString()
                )
            ;
            result = "DELETE FROM ks.tb WHERE v1 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Delete()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetWhereVariables(Columns.columns1)
                    .SetIfExists()
                    .ToString()
                )
            ;

            result = "DELETE v1 FROM ks.tb WHERE v2 = ?;";
            Assert.AreEqual(result,
                new Delete()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1)
                    .SetWhereVariables(Columns.columns2)
                    .ToString()
                )
            ;
            result = "DELETE v1 FROM ks.tb WHERE v2 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Delete()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1)
                    .SetWhereVariables(Columns.columns2)
                    .SetIfExists()
                    .ToString()
                )
            ;

            result = "DELETE v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Delete()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .ToString()
                )
            ;
            result = "DELETE v1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Delete()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetIfExists()
                    .ToString()
                )
            ;
            
            result = "DELETE v1, v2 FROM ks.tb USING TIMESTAMP ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Delete()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetIfExists()
                    .SetTimestamp()
                    .ToString()
                )
            ;

            result = "DELETE vl1[?], v2 FROM ks.tb WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Delete()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns_list1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetListDeleteType(ListDeleteType.SELECTED)
                    .ToString()
                )
            ;

            result = "DELETE vl1, v2 FROM ks.tb WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Delete()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetVariables(Columns.columns_list1, Columns.columns2)
                    .SetWhereVariables(Columns.columns1, Columns.columns3)
                    .SetListDeleteType(ListDeleteType.ALL)
                    .ToString()
                )
            ;
        }

        [TestMethod]
        public void UT_Delete_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new Delete()
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new Delete()
                        .SetKeyspace(Variables.keyspace)
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new Delete()
                        .SetKeyspace(Variables.keyspace)
                        .SetTableName(Tables.tableName)
                        .ToString();
                })
            );
        }


    }
}