using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CassandraQueryBuilder;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_MaterializedViews
    {
        [TestMethod]
        public void UT_MaterializedViews_GetString()
        {
            String result = "CREATE MATERIALIZED VIEW ks.mv AS SELECT pk1, pk2 FROM ks.tb WHERE pk1 IS NOT NULL AND pk2 IS NOT NULL PRIMARY KEY (pk1, pk2);";
            Assert.AreEqual(result,
                new CreateMaterializedView()
                    .SetKeyspace(Variables.keyspace)
                    .SetToTableName(Tables.materializedViewName)
                    .SetFromTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1, Columns.partitionKey2)
                    .ToString()
            );

            result = "CREATE MATERIALIZED VIEW ks.mv AS SELECT pk1, ck1, ck2, v1, v2 FROM ks.tb WHERE pk1 IS NOT NULL AND ck1 IS NOT NULL AND ck2 IS NOT NULL PRIMARY KEY ((pk1), ck1, ck2);";
            Assert.AreEqual(result,
                new CreateMaterializedView()
                    .SetKeyspace(Variables.keyspace)
                    .SetToTableName(Tables.materializedViewName)
                    .SetFromTableName(Tables.tableName)
                    .SetPartitionKeys(Columns.partitionKey1)
                    .SetClusteringKeys(Columns.clusteringKey1, Columns.clusteringKey2)
                    .SetColumns(Columns.columns1, Columns.columns2)
                    .ToString()
            );
        }

        [TestMethod]
        public void UT_MaterializedViews_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateMaterializedView()
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateMaterializedView()
                        .SetKeyspace(Variables.keyspace)
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateMaterializedView()
                        .SetKeyspace(Variables.keyspace)
                        .SetToTableName(Tables.materializedViewName)
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateMaterializedView()
                        .SetKeyspace(Variables.keyspace)
                        .SetToTableName(Tables.materializedViewName)
                        .SetFromTableName(Tables.tableName)
                        .ToString();
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateMaterializedView()
                        .SetKeyspace(Variables.keyspace)
                        .SetToTableName(Tables.materializedViewName)
                        .SetFromTableName(Tables.tableName)
                        .SetPartitionKeys(Columns.partitionKey1)
                        .SetClusteringKeysOrderByASC(false)
                        .ToString();
                }
            );

            Assert.ThrowsException<Exception>(
                () => {
                    new CreateMaterializedView()
                        .SetKeyspace(Variables.keyspace)
                        .SetToTableName(Tables.materializedViewName)
                        .SetFromTableName(Tables.tableName)
                        .SetPartitionKeys(Columns.partitionKey1)
                        .SetClusteringKeys(Columns.clusteringKey1)
                        .SetClusteringKeysOrderByASC(false, true)
                        .ToString();
                }
            );
        }


    }
}
