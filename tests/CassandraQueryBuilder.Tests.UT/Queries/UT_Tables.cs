using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CassandraQueryBuilder;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_Tables
    {

        
        [TestMethod]
        public void UT_Tables_GetString()
        {
            String result = "CREATE TABLE ks.tb (pk1 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY (pk1));";
            Assert.AreEqual(result,
                new CreateTable()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1)
                    .SetColumns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .GetString()
            );
            
            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY (pk1, pk2));";
            Assert.AreEqual(result,
                new CreateTable()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .SetColumns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .GetString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, ck1 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1), ck1));";
            Assert.AreEqual(result,
                new CreateTable()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1)
                    .SetClusteringKeys(Columns.clusteringKey1)
                    .SetColumns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .GetString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1, pk2), ck1));";
            Assert.AreEqual(result,
                new CreateTable()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .SetClusteringKeys(Columns.clusteringKey1)
                    .SetColumns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .GetString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, ck1 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1), ck1)) WITH CLUSTERING ORDER BY (ck1 ASC);";
            Assert.AreEqual(result,
                new CreateTable()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1)
                    .SetClusteringKeys(Columns.clusteringKey1)
                    .SetClusteringKeysOrderByASC(true)
                    .SetColumns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .GetString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, ck1 TEXT, ck2 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1), ck1, ck2)) WITH CLUSTERING ORDER BY (ck1 DESC);";
            Assert.AreEqual(result,
                new CreateTable()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1)
                    .SetClusteringKeys(Columns.clusteringKey1, Columns.clusteringKey2)
                    .SetClusteringKeysOrderByASC(false)
                    .SetColumns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .GetString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, ck1 TEXT, ck2 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1), ck1, ck2)) WITH CLUSTERING ORDER BY (ck1 DESC, ck2 ASC);";
            Assert.AreEqual(result,
                new CreateTable()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1)
                    .SetClusteringKeys(Columns.clusteringKey1, Columns.clusteringKey2)
                    .SetClusteringKeysOrderByASC(false, true)
                    .SetColumns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .GetString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, ck1 TEXT, ck2 TEXT, ck3 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1), ck1, ck2, ck3)) WITH CLUSTERING ORDER BY (ck1 DESC);";
            Assert.AreEqual(result,
                new CreateTable()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1)
                    .SetClusteringKeys(Columns.clusteringKey1, Columns.clusteringKey2, Columns.clusteringKey3)
                    .SetClusteringKeysOrderByASC(false)
                    .SetColumns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .GetString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, ck1 TEXT, ck2 TEXT, ck3 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1), ck1, ck2, ck3)) WITH CLUSTERING ORDER BY (ck1 DESC, ck2 ASC, ck3 DESC);";
            Assert.AreEqual(result,
                new CreateTable()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1)
                    .SetClusteringKeys(Columns.clusteringKey1, Columns.clusteringKey2, Columns.clusteringKey3)
                    .SetClusteringKeysOrderByASC(false, true, false)
                    .SetColumns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .GetString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, ck2 TEXT, ck3 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1, pk2), ck1, ck2, ck3)) WITH CLUSTERING ORDER BY (ck1 DESC, ck2 ASC, ck3 DESC);";
            Assert.AreEqual(result,
                new CreateTable()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .SetClusteringKeys(Columns.clusteringKey1, Columns.clusteringKey2, Columns.clusteringKey3)
                    .SetClusteringKeysOrderByASC(false, true, false)
                    .SetColumns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .GetString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, ck2 TEXT, ck3 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, v4 TEXT STATIC, PRIMARY KEY ((pk1, pk2), ck1, ck2, ck3)) WITH CLUSTERING ORDER BY (ck1 DESC, ck2 ASC, ck3 DESC);";
            Assert.AreEqual(result,
                new CreateTable()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .SetClusteringKeys(Columns.clusteringKey1, Columns.clusteringKey2, Columns.clusteringKey3)
                    .SetClusteringKeysOrderByASC(false, true, false)
                    .SetColumns(Columns.columns1, Columns.columns2, Columns.columns3, Columns.columns4_STATIC)
                    .GetString()
            );


            //---- Test compaction strategy


            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1, pk2), ck1)) WITH compaction = { 'class' : 'SizeTieredCompactionStrategy' };";
            Assert.AreEqual(result,
                new CreateTable()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .SetClusteringKeys(Columns.clusteringKey1)
                    .SetColumns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .SetCompactionStrategy(CompactionStrategy.SizeTieredCompactionStrategy)
                    .GetString()
            );

            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, ck2 TEXT, ck3 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, v4 TEXT STATIC, PRIMARY KEY ((pk1, pk2), ck1, ck2, ck3)) WITH CLUSTERING ORDER BY (ck1 DESC, ck2 ASC, ck3 DESC) AND compaction = { 'class' : 'LeveledCompactionStrategy' };";
            Assert.AreEqual(result,
                new CreateTable()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .SetClusteringKeys(Columns.clusteringKey1, Columns.clusteringKey2, Columns.clusteringKey3)
                    .SetClusteringKeysOrderByASC(false, true, false)
                    .SetColumns(Columns.columns1, Columns.columns2, Columns.columns3, Columns.columns4_STATIC)
                    .SetCompactionStrategy(CompactionStrategy.LeveledCompactionStrategy)
                    .GetString()
            );


            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, ck2 TEXT, ck3 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, v4 TEXT STATIC, PRIMARY KEY ((pk1, pk2), ck1, ck2, ck3)) WITH CLUSTERING ORDER BY (ck1 DESC, ck2 ASC, ck3 DESC) AND compaction = { 'class' : 'DateTieredCompactionStrategy' };";
            Assert.AreEqual(result,
                new CreateTable()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .SetClusteringKeys(Columns.clusteringKey1, Columns.clusteringKey2, Columns.clusteringKey3)
                    .SetClusteringKeysOrderByASC(false, true, false)
                    .SetColumns(Columns.columns1, Columns.columns2, Columns.columns3, Columns.columns4_STATIC)
                    .SetCompactionStrategy(CompactionStrategy.DateTieredCompactionStrategy)
                    .GetString()
            );


            //---- Test gc_grace_seconds


            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, v1 TEXT, v2 TEXT, v3 TEXT, PRIMARY KEY ((pk1, pk2), ck1)) WITH compaction = { 'class' : 'SizeTieredCompactionStrategy' } AND gc_grace_seconds = 864000;";
            Assert.AreEqual(result,
                new CreateTable()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .SetClusteringKeys(Columns.clusteringKey1)
                    .SetColumns(Columns.columns1, Columns.columns2, Columns.columns3)
                    .SetCompactionStrategy(CompactionStrategy.SizeTieredCompactionStrategy)
                    .SetGcGrace(864000)
                    .GetString()
            );


            //---- Test list<T>


            result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, ck1 TEXT, v1 TEXT, vl1 LIST<TEXT>, v3 TEXT, PRIMARY KEY ((pk1, pk2), ck1));";
            Assert.AreEqual(result,
                new CreateTable()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .SetClusteringKeys(Columns.clusteringKey1)
                    .SetColumns(Columns.columns1, Columns.columns_list1, Columns.columns3)
                    .GetString()
            );

        }

        [TestMethod]
        public void UT_Tables_WithPropertiesAlreadySet()
        {
            Assert.ThrowsException<Exception>(
                () => {
                    new CreateTable()
                        .SetKeyspace(Variables.keyspace)
                        .SetTableName(Tables.tableName)
                        .SetPartitionKeys(Columns.partitionKey1)
                        .SetClusteringKeys(Columns.clusteringKey1)
                        .SetClusteringKeysOrderByASC(false)
                        .SetClusteringKeysOrderByASC(false)
                        .GetString();
                }
            );

            Assert.ThrowsException<Exception>(
                () => {
                    new CreateTable()
                        .SetKeyspace(Variables.keyspace)
                        .SetTableName(Tables.tableName)
                        .SetPartitionKeys(Columns.partitionKey1)
                        .SetClusteringKeys(Columns.clusteringKey1)
                        .SetCompactionStrategy(CompactionStrategy.DateTieredCompactionStrategy)
                        .SetCompactionStrategy(CompactionStrategy.DateTieredCompactionStrategy)
                        .GetString();
                }
            );


            Assert.ThrowsException<Exception>(
                () => {
                    new CreateTable()
                        .SetKeyspace(Variables.keyspace)
                        .SetTableName(Tables.tableName)
                        .SetPartitionKeys(Columns.partitionKey1)
                        .SetClusteringKeys(Columns.clusteringKey1)
                        .SetGcGrace(10)
                        .SetGcGrace(10)
                        .GetString();
                }
            );

        }

        [TestMethod]
        public void UT_Tables_GetStringForCounter()
        {
            String result = "CREATE TABLE ks.tb (pk1 TEXT, pk2 TEXT, counter_column_name COUNTER, PRIMARY KEY (pk1, pk2));";
            Assert.AreEqual(result,
                new CreateTable()
                    .SetKeyspace(Variables.keyspace)
                    .SetTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .SetColumns(Columns.columnsCounter)
                    .GetString()
            );
        }

        [TestMethod]
        public void UT_Tables_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateTable()
                        .GetString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateTable()
                        .SetKeyspace(Variables.keyspace)
                        .GetString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateTable()
                        .SetKeyspace(Variables.keyspace)
                        .SetTableName(Tables.tableName)
                        .GetString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateTable()
                        .SetKeyspace(Variables.keyspace)
                        .SetTableName(Tables.tableName)
                        .SetPartitionKeys(Columns.partitionKey1)
                        .SetClusteringKeysOrderByASC(false)
                        .GetString();
                }
            );

            Assert.ThrowsException<Exception>(
                () => {
                    new CreateTable()
                        .SetKeyspace(Variables.keyspace)
                        .SetTableName(Tables.tableName)
                        .SetPartitionKeys(Columns.partitionKey1)
                        .SetClusteringKeys(Columns.clusteringKey1)
                        .SetClusteringKeysOrderByASC(false, true)
                        .GetString();
                }
            );
        }


    }
}
