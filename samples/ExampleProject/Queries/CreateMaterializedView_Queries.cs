using System;
using CassandraQueryBuilder;

namespace ExampleProject.Queries
{
    public class CreateMaterializedView_Queries
    {

        //"CREATE MATERIALIZED VIEW my_keyspace.users_mv AS SELECT email, user_id FROM my_keyspace.users WHERE email IS NOT NULL AND user_id IS NOT NULL PRIMARY KEY ((email) user_id);";
        internal static readonly String USER_TABLE = new CreateMaterializedView()
            .Keyspace(DBVariables.KEYSPACE)
            .ToTable(DBTables.USERS_MATERIALIZED_VIEW)
            .FromTable(DBTables.USERS)
            .PartitionKeys(DBColumns.EMAIL)
            .ClusteringKeys(DBColumns.USER_ID)
            .ToString();

    }
}
