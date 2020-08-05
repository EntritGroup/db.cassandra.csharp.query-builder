using System;
using CassandraQueryBuilder;

namespace ExampleProject.Queries
{
    public class Update_Queries
    {

        //"UPDATE my_keyspace.users SET name = ? WHERE user_id = ?;"
        internal static readonly String UPDATE_USER_NAME = new Update()
            .Keyspace(DBVariables.KEYSPACE)
            .Table(DBTables.USERS)
            .UpdateColumns(DBColumns.NAME)
            .WhereColumns(DBColumns.USER_ID)
            .ToString();

    }
}
