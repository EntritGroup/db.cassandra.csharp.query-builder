using System;
using CassandraQueryBuilder;

namespace ExampleProject.Queries
{
    public class Delete_Queries
    {
        //"DELETE FROM my_keyspace.users WHERE user_id = ?;"
        internal static readonly String DELETE_USER = new Delete()
            .Keyspace(DBVariables.KEYSPACE)
            .Table(DBTables.USERS)
            .WhereColumns(DBColumns.USER_ID)
            .ToString();
    }
}
