using System;
using CassandraQueryBuilder;

namespace ExampleProject.Queries
{
    public class DropKeyspace_Queries
    {
        //"DROP KEYSPACE IF EXISTS my_keyspace;";
        internal static readonly String DROP_KEYSPACE = new DropKeyspace()
            .Keyspace(DBVariables.KEYSPACE)
            .ToString();      

    }
}
