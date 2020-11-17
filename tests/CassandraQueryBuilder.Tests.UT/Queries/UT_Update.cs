using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


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
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1)
                    .WhereColumns(Columns.columns2)
                    .ToString()
            );

            result = "UPDATE ks.tb SET v1 = ? WHERE v2 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1)
                    .WhereColumns(Columns.columns2)
                    .IfExists()
                    .ToString()
            );
            
            result = "UPDATE ks.tb SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .ToString()
            );

            result = "UPDATE ks.tb SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .IfExists()
                    .ToString()
            );

            result = "UPDATE ks.tb USING TTL ? SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .IfExists()
                    .TTL()
                    .ToString()
            );

            result = "UPDATE ks.tb USING TIMESTAMP ? SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .IfExists()
                    .Timestamp()
                    .ToString()
            );

            result = "UPDATE ks.tb USING TIMESTAMP ? AND TTL ? SET v1 = ?, v2 = ? WHERE v1 = ? AND v3 = ? IF EXISTS;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .IfExists()
                    .Timestamp()
                    .TTL()
                    .ToString()
            );


            //---- List Frozen Tuple
            
            result = "UPDATE ks.tb SET vl1 = vl1 + ? WHERE v1 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns_list1)
                    .ListUpdateTypes(ListUpdateType.APPEND)
                    .WhereColumns(Columns.columns1)
                    .ToString()
            );

            result = "UPDATE ks.tb SET vl1 = ? + vl1 WHERE v1 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns_list1)
                    .ListUpdateTypes(ListUpdateType.PREPEND)
                    .WhereColumns(Columns.columns1)
                    .ToString()
            );
            
            result = "UPDATE ks.tb SET vl1[?] = ? WHERE v1 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns_list1)
                    .ListUpdateTypes(ListUpdateType.SPECIFY_INDEX_TO_OVERWRITE)
                    .WhereColumns(Columns.columns1)
                    .ToString()
            );

            result = "UPDATE ks.tb SET list_frozen_tuple = ? WHERE v1 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.LIST_FROZEN_TUPLE)
                    .ListUpdateTypes(ListUpdateType.REPLACE_ALL)
                    .WhereColumns(Columns.columns1)
                    .ToString()
            );
            
        }

        [TestMethod]
        public void UT_Update_Map_GetString()
        {
            String result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vm1 = vm1 + ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2, Columns.columns_map1)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .MapUpdateTypes(MapUpdateType.ADD)
                    .ToString()
            );

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vm1 = vm1 - ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2, Columns.columns_map1)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .MapUpdateTypes(MapUpdateType.REMOVE)
                    .ToString()
            );
            
            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vm1 = vm1 + ?, vm2 = vm2 - ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2, Columns.columns_map1, Columns.columns_map2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .MapUpdateTypes(MapUpdateType.ADD, MapUpdateType.REMOVE)
                    .ToString()
            );
        }

        [TestMethod]
        public void UT_Update_Set_GetString()
        {
            String result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vs1 = vs1 + ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2, Columns.columns_set1)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .SetUpdateTypes(SetUpdateType.ADD)
                    .ToString()
            );

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vs1 = vs1 - ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2, Columns.columns_set1)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .SetUpdateTypes(SetUpdateType.REMOVE)
                    .ToString()
            );

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vs1 = vs1 + ?, vs2 = vs2 - ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2, Columns.columns_set1, Columns.columns_set2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .SetUpdateTypes(SetUpdateType.ADD, SetUpdateType.REMOVE)
                    .ToString()
            );
        }
        
        [TestMethod]
        public void UT_Update_List_GetString()
        {
            String result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vl1 = ? + vl1 WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2, Columns.columns_list1)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .ListUpdateTypes(ListUpdateType.PREPEND)
                    .ToString()
            );

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vl1 = vl1 + ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2, Columns.columns_list1)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .ListUpdateTypes(ListUpdateType.APPEND)
                    .ToString()
            );

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vl1 = ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2, Columns.columns_list1)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .ListUpdateTypes(ListUpdateType.REPLACE_ALL)
                    .ToString()
            );

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vl1[?] = ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2, Columns.columns_list1)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .ListUpdateTypes(ListUpdateType.SPECIFY_INDEX_TO_OVERWRITE)
                    .ToString()
            );

            result = "UPDATE ks.tb SET v1 = ?, v2 = ?, vl1 = ? + vl1, vl2 = vl2 + ? WHERE v1 = ? AND v3 = ?;";
            Assert.AreEqual(result,
                new Update()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .UpdateColumns(Columns.columns1, Columns.columns2, Columns.columns_list1, Columns.columns_list2)
                    .WhereColumns(Columns.columns1, Columns.columns3)
                    .ListUpdateTypes(ListUpdateType.PREPEND, ListUpdateType.APPEND)
                    .ToString()
            );

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
                        .Keyspace(Variables.keyspace)
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new Update()
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .ToString();
                })
            );

            Assert.ThrowsException<NullReferenceException>(
(Action)(() => {
                    new Update()
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .UpdateColumns(Columns.columns1)
                        .ToString();
                })
            );
        }


    }
}
