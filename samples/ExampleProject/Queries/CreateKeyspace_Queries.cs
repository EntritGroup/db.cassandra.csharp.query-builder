using System;
using CassandraQueryBuilder;

namespace ExampleProject.Queries
{
    public class CreateKeyspace_Queries
    {
        //"CREATE KEYSPACE my_keyspace WITH REPLICATION = { 'class' : 'NetworkTopologyStrategy', 'dc1' : 3 };";
        internal static readonly String CREATE_KEYSPACE = new CreateKeyspace()
            .Keyspace(DBVariables.KEYSPACE)
            .ReplicationStrategy(ReplicationStrategy.NetworkTopologyStrategy)
            .DataCenters(new DataCenter[] { new DataCenter(DBVariables.DATA_CENTER_NAME, 3) })
            .ToString();
    }
}
