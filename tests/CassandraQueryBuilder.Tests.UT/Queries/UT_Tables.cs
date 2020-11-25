using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_Tables
    {

        
        [TestMethod]
        public void UT_Tables_GetString()
        {
            String result = "CREATE TABLE ks.tb (pk1 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1)));";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1)
                    .Columns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .ToString()
            );
            
            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1, pk2)));";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .Columns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .ToString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, ck1 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1), ck1));";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1)
                    .ClusteringKeys(Columns.clusteringKey1)
                    .Columns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .ToString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1, pk2), ck1));";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .ClusteringKeys(Columns.clusteringKey1)
                    .Columns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .ToString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, ck1 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1), ck1)) WITH CLUSTERING ORDER BY (ck1 ASC);";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1)
                    .ClusteringKeys(Columns.clusteringKey1)
                    .ClusteringKeysOrderByASC(true)
                    .Columns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .ToString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, ck1 TEXT, ck2 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1), ck1, ck2)) WITH CLUSTERING ORDER BY (ck1 DESC);";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1)
                    .ClusteringKeys(Columns.clusteringKey1, Columns.clusteringKey2)
                    .ClusteringKeysOrderByASC(false)
                    .Columns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .ToString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, ck1 TEXT, ck2 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1), ck1, ck2)) WITH CLUSTERING ORDER BY (ck1 DESC, ck2 ASC);";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1)
                    .ClusteringKeys(Columns.clusteringKey1, Columns.clusteringKey2)
                    .ClusteringKeysOrderByASC(false, true)
                    .Columns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .ToString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, ck1 TEXT, ck2 TEXT, ck3 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1), ck1, ck2, ck3)) WITH CLUSTERING ORDER BY (ck1 DESC);";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1)
                    .ClusteringKeys(Columns.clusteringKey1, Columns.clusteringKey2, Columns.clusteringKey3)
                    .ClusteringKeysOrderByASC(false)
                    .Columns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .ToString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, ck1 TEXT, ck2 TEXT, ck3 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1), ck1, ck2, ck3)) WITH CLUSTERING ORDER BY (ck1 DESC, ck2 ASC, ck3 DESC);";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1)
                    .ClusteringKeys(Columns.clusteringKey1, Columns.clusteringKey2, Columns.clusteringKey3)
                    .ClusteringKeysOrderByASC(false, true, false)
                    .Columns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .ToString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, ck2 TEXT, ck3 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1, pk2), ck1, ck2, ck3)) WITH CLUSTERING ORDER BY (ck1 DESC, ck2 ASC, ck3 DESC);";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .ClusteringKeys(Columns.clusteringKey1, Columns.clusteringKey2, Columns.clusteringKey3)
                    .ClusteringKeysOrderByASC(false, true, false)
                    .Columns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .ToString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, ck2 TEXT, ck3 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, v4 TEXT STATIC, PRIMARY KEY ((pk1, pk2), ck1, ck2, ck3)) WITH CLUSTERING ORDER BY (ck1 DESC, ck2 ASC, ck3 DESC);";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .ClusteringKeys(Columns.clusteringKey1, Columns.clusteringKey2, Columns.clusteringKey3)
                    .ClusteringKeysOrderByASC(false, true, false)
                    .Columns(Columns.columns1, Columns.columns2, Columns.columns3, Columns.columns4_STATIC)
                    .ToString()
            );


            //---- Test compaction strategy


            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1, pk2), ck1)) WITH compaction = { 'class' : 'SizeTieredCompactionStrategy' };";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .ClusteringKeys(Columns.clusteringKey1)
                    .Columns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .CompactionStrategy(CompactionStrategy.SizeTieredCompactionStrategy)
                    .ToString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, ck2 TEXT, ck3 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, v4 TEXT STATIC, PRIMARY KEY ((pk1, pk2), ck1, ck2, ck3)) WITH CLUSTERING ORDER BY (ck1 DESC, ck2 ASC, ck3 DESC) AND compaction = { 'class' : 'LeveledCompactionStrategy' };";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .ClusteringKeys(Columns.clusteringKey1, Columns.clusteringKey2, Columns.clusteringKey3)
                    .ClusteringKeysOrderByASC(false, true, false)
                    .Columns(Columns.columns1, Columns.columns2, Columns.columns3, Columns.columns4_STATIC)
                    .CompactionStrategy(CompactionStrategy.LeveledCompactionStrategy)
                    .ToString()
            );


            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, ck2 TEXT, ck3 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, v4 TEXT STATIC, PRIMARY KEY ((pk1, pk2), ck1, ck2, ck3)) WITH CLUSTERING ORDER BY (ck1 DESC, ck2 ASC, ck3 DESC) AND compaction = { 'class' : 'TimeWindowCompactionStrategy' };";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .ClusteringKeys(Columns.clusteringKey1, Columns.clusteringKey2, Columns.clusteringKey3)
                    .ClusteringKeysOrderByASC(false, true, false)
                    .Columns(Columns.columns1, Columns.columns2, Columns.columns3, Columns.columns4_STATIC)
                    .CompactionStrategy(CompactionStrategy.TimeWindowCompactionStrategy)
                    .ToString()
            );


            //---- Test gc_grace_seconds


            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1, pk2), ck1)) WITH compaction = { 'class' : 'SizeTieredCompactionStrategy' } AND gc_grace_seconds = 864000;";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .ClusteringKeys(Columns.clusteringKey1)
                    .Columns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .CompactionStrategy(CompactionStrategy.SizeTieredCompactionStrategy)
                    .GcGrace(864000)
                    .ToString()
            );


            //---- Test MAP<T, U>


            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, v1 TEXT, vm1 MAP<TEXT, TEXT>, v3 TEXT, PRIMARY KEY ((pk1, pk2), ck1));";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .ClusteringKeys(Columns.clusteringKey1)
                    .Columns(Columns.columns1, Columns.columns_map1, Columns.columns3)
                    .ToString()
            );


            //---- Test SET<T>


            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, v1 TEXT, vs1 SET<TEXT>, v3 TEXT, PRIMARY KEY ((pk1, pk2), ck1));";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .ClusteringKeys(Columns.clusteringKey1)
                    .Columns(Columns.columns1, Columns.columns_set1, Columns.columns3)
                    .ToString()
            );


            //---- Test list<T>


            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, v1 TEXT, vl1 LIST<TEXT>, v3 TEXT, PRIMARY KEY ((pk1, pk2), ck1));";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .ClusteringKeys(Columns.clusteringKey1)
                    .Columns(Columns.columns1, Columns.columns_list1, Columns.columns3)
                    .ToString()
            );


            //---- Frozen and Tuple
            

            result = "CREATE TABLE ks.tb (pk1 TEXT, v1 TEXT, fl1 FROZEN<LIST<TEXT>>, v3 TEXT, PRIMARY KEY ((pk1)));";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1)
                    .Columns(Columns.columns1, Columns.frozen_list1, Columns.columns3)
                    .ToString()
            );
            
            result = "CREATE TABLE ks.tb (pk1 TEXT, v1 TEXT, fl1 FROZEN<LIST<TEXT>>, t1 TUPLE<TEXT, BOOLEAN>, PRIMARY KEY ((pk1)));";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1)
                    .Columns(Columns.columns1, Columns.frozen_list1, Columns.tuple1)
                    .ToString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, v1 TEXT, fl1 FROZEN<LIST<TEXT>>, t1 TUPLE<TEXT, BOOLEAN>, ft1 FROZEN<TUPLE<TEXT, BOOLEAN>>, PRIMARY KEY ((pk1)));";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1)
                    .Columns(Columns.columns1, Columns.frozen_list1, Columns.tuple1, Columns.frozen_tuple1)
                    .ToString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, v1 TEXT, fl1 FROZEN<LIST<TEXT>>, t1 TUPLE<TEXT, BOOLEAN>, ft1 FROZEN<TUPLE<TEXT, BOOLEAN>>, lft1 LIST<FROZEN<TUPLE<TEXT, BOOLEAN>>>, PRIMARY KEY ((pk1)));";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1)
                    .Columns(Columns.columns1, Columns.frozen_list1, Columns.tuple1, Columns.frozen_tuple1, Columns.list_frozen_tuple1)
                    .ToString()
            );

        }

        [TestMethod]
        public void UT_Tables_WithPropertiesAlreadySet()
        {
            Assert.ThrowsException<Exception>(
                () => {
                    new CreateTable()
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .PartitionKeys(Columns.partitionKey1)
                        .ClusteringKeys(Columns.clusteringKey1)
                        .ClusteringKeysOrderByASC(false)
                        .ClusteringKeysOrderByASC(false)
                        .ToString();
                }
            );

            Assert.ThrowsException<Exception>(
                () => {
                    new CreateTable()
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .PartitionKeys(Columns.partitionKey1)
                        .ClusteringKeys(Columns.clusteringKey1)
                        .CompactionStrategy(CompactionStrategy.TimeWindowCompactionStrategy)
                        .CompactionStrategy(CompactionStrategy.TimeWindowCompactionStrategy)
                        .ToString();
                }
            );


            Assert.ThrowsException<Exception>(
                () => {
                    new CreateTable()
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .PartitionKeys(Columns.partitionKey1)
                        .ClusteringKeys(Columns.clusteringKey1)
                        .GcGrace(10)
                        .GcGrace(10)
                        .ToString();
                }
            );

        }

        [TestMethod]
        public void UT_Tables_GetStringForCounter()
        {
            String result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, c1 COUNTER, PRIMARY KEY ((pk1, pk2)));";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .Columns(Columns.columnsCounter1)
                    .ToString()
            );
            
            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, c1 COUNTER, c2 COUNTER, PRIMARY KEY ((pk1, pk2)));";
            Assert.AreEqual(result,
                new CreateTable()
                    .Keyspace(Variables.keyspace)
                    .Table(Tables.tableName)
                    .PartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .Columns(Columns.columnsCounter1, Columns.columnsCounter2)
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
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateTable()
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .PartitionKeys(Columns.partitionKey1)
                        .ClusteringKeysOrderByASC(false)
                        .ToString();
                }
            );

            Assert.ThrowsException<Exception>(
                () => {
                    new CreateTable()
                        .Keyspace(Variables.keyspace)
                        .Table(Tables.tableName)
                        .PartitionKeys(Columns.partitionKey1)
                        .ClusteringKeys(Columns.clusteringKey1)
                        .ClusteringKeysOrderByASC(false, true)
                        .ToString();
                }
            );
        }


    }
}
