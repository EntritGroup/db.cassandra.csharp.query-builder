using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CassandraQueryBuilder;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_CreateKeyspace
    {
        [TestMethod]
        public void UT_CreateKeyspace_GetString()
        {
            String result = "CREATE KEYSPACE ks WITH REPLICATION = { 'class' : 'SimpleStrategy', 'replication_factor' : 3 };";
            Assert.AreEqual(result,
                new CreateKeyspace()
                    .SetName(Variables.keyspace)
                    .SetReplicationStrategy(ReplicationStrategy.SimpleStrategy)
                    .SetDataCenters(new DataCenter[] { new DataCenter(Variables.dataCenterName1, 3) })
                    .GetString()
                )
            ;

            result = "CREATE KEYSPACE ks WITH REPLICATION = { 'class' : 'NetworkTopologyStrategy', 'dc1' : 3 };";
            Assert.AreEqual(result,
                new CreateKeyspace()
                    .SetName(Variables.keyspace)
                    .SetReplicationStrategy(ReplicationStrategy.NetworkTopologyStrategy)
                    .SetDataCenters(new DataCenter[] { new DataCenter(Variables.dataCenterName1, 3) })
                    .GetString()
                )
            ;

            result = "CREATE KEYSPACE ks WITH REPLICATION = { 'class' : 'NetworkTopologyStrategy', 'dc1' : 3, 'dc2' : 2 };";
            Assert.AreEqual(result,
                new CreateKeyspace()
                    .SetName(Variables.keyspace)
                    .SetReplicationStrategy(ReplicationStrategy.NetworkTopologyStrategy)
                    .SetDataCenters(new DataCenter[] { new DataCenter(Variables.dataCenterName1, 3), new DataCenter(Variables.dataCenterName2, 2) })
                    .GetString()
                )
            ;
        }

        [TestMethod]
        public void UT_CreateKeyspace_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateKeyspace()
                        .GetString()
                    ;
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateKeyspace()
                        .SetName(Variables.keyspace)
                        .GetString()
                    ;
                }
            );

            
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateKeyspace()
                        .SetName(Variables.keyspace)
                        .SetReplicationStrategy(ReplicationStrategy.SimpleStrategy)
                        .GetString()
                    ;
                }
            );

            //--- Simple Strategy

            Assert.ThrowsException<Exception>(
                () => {
                    new CreateKeyspace()
                        .SetName(Variables.keyspace)
                        .SetReplicationStrategy(ReplicationStrategy.SimpleStrategy)
                        .SetDataCenters(new DataCenter[0])
                        .GetString()
                    ;
                }
            );

            Assert.ThrowsException<Exception>(
                () => {
                    new CreateKeyspace()
                        .SetName(Variables.keyspace)
                        .SetReplicationStrategy(ReplicationStrategy.SimpleStrategy)
                        .SetDataCenters(new DataCenter[2])
                        .GetString()
                    ;
                }
            );

            //--- Network Topology Strategy

            Assert.ThrowsException<Exception>(
                () => {
                    new CreateKeyspace()
                        .SetName(Variables.keyspace)
                        .SetReplicationStrategy(ReplicationStrategy.NetworkTopologyStrategy)
                        .SetDataCenters(new DataCenter[0])
                        .GetString()
                    ;
                }
            );



        }

        


    }
}
