using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_CreateKeyspace
    {
        //https://cassandra.apache.org/doc/latest/cql/ddl.html
        [TestMethod]
        public void UT_CreateKeyspace_GetString()
        {
            String result = "CREATE KEYSPACE ks WITH REPLICATION = { 'class' : 'SimpleStrategy', 'replication_factor' : 3 };";
            Assert.AreEqual(result,
                new CreateKeyspace()
                    .Keyspace(Variables.keyspace)
                    .ReplicationStrategy(ReplicationStrategy.SimpleStrategy)
                    .DataCenters(new DataCenter[] { new DataCenter(Variables.dataCenterName1, 3) })
                    .ToString()
            );

            result = "CREATE KEYSPACE ks WITH REPLICATION = { 'class' : 'SimpleStrategy', 'replication_factor' : 3 };";
            Assert.AreEqual(result,
                new CreateKeyspace()
                    .Keyspace(Variables.keyspace)
                    .ReplicationStrategy(ReplicationStrategy.SimpleStrategy)
                    .DataCenters(new DataCenter[] { new DataCenter(null, 3) })
                    .ToString()
            );

            result = "CREATE KEYSPACE ks WITH REPLICATION = { 'class' : 'NetworkTopologyStrategy', 'dc1' : 3 };";
            Assert.AreEqual(result,
                new CreateKeyspace()
                    .Keyspace(Variables.keyspace)
                    .ReplicationStrategy(ReplicationStrategy.NetworkTopologyStrategy)
                    .DataCenters(new DataCenter[] { new DataCenter(Variables.dataCenterName1, 3) })
                    .ToString()
            );

            result = "CREATE KEYSPACE ks WITH REPLICATION = { 'class' : 'NetworkTopologyStrategy', 'dc1' : 3, 'dc2' : 2 };";
            Assert.AreEqual(result,
                new CreateKeyspace()
                    .Keyspace(Variables.keyspace)
                    .ReplicationStrategy(ReplicationStrategy.NetworkTopologyStrategy)
                    .DataCenters(new DataCenter[] { new DataCenter(Variables.dataCenterName1, 3), new DataCenter(Variables.dataCenterName2, 2) })
                    .ToString()
            );
            
            result = "CREATE KEYSPACE ks WITH REPLICATION = { 'class' : 'NetworkTopologyStrategy', replication_factor : 3 };";
            Assert.AreEqual(result,
                new CreateKeyspace()
                    .Keyspace(Variables.keyspace)
                    .ReplicationStrategy(ReplicationStrategy.NetworkTopologyStrategy)
                    .DataCenters(new DataCenter[] { new DataCenter(null, 3) })
                    .ToString()
            );
            
            result = "CREATE KEYSPACE ks WITH REPLICATION = { 'class' : 'NetworkTopologyStrategy', replication_factor : 3, 'dc2' : 2 };";
            Assert.AreEqual(result,
                new CreateKeyspace()
                    .Keyspace(Variables.keyspace)
                    .ReplicationStrategy(ReplicationStrategy.NetworkTopologyStrategy)
                    .DataCenters(new DataCenter[] { new DataCenter(null, 3), new DataCenter(Variables.dataCenterName2, 2) })
                    .ToString()
            );

        }

        [TestMethod]
        public void UT_CreateKeyspace_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateKeyspace()
                        .ToString()
                    ;
                }
            );

            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateKeyspace()
                        .Keyspace(Variables.keyspace)
                        .ToString()
                    ;
                }
            );

            
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new CreateKeyspace()
                        .Keyspace(Variables.keyspace)
                        .ReplicationStrategy(ReplicationStrategy.SimpleStrategy)
                        .ToString()
                    ;
                }
            );

            //--- Simple Strategy

            Assert.ThrowsException<Exception>(
                () => {
                    new CreateKeyspace()
                        .Keyspace(Variables.keyspace)
                        .ReplicationStrategy(ReplicationStrategy.SimpleStrategy)
                        .DataCenters(new DataCenter[0])
                        .ToString()
                    ;
                }
            );

            Assert.ThrowsException<Exception>(
                () => {
                    new CreateKeyspace()
                        .Keyspace(Variables.keyspace)
                        .ReplicationStrategy(ReplicationStrategy.SimpleStrategy)
                        .DataCenters(new DataCenter[2])
                        .ToString()
                    ;
                }
            );

            //--- Network Topology Strategy

            Assert.ThrowsException<Exception>(
                () => {
                    new CreateKeyspace()
                        .Keyspace(Variables.keyspace)
                        .ReplicationStrategy(ReplicationStrategy.NetworkTopologyStrategy)
                        .DataCenters(new DataCenter[0])
                        .ToString()
                    ;
                }
            );



        }

        


    }
}
