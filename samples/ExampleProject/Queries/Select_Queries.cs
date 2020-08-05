using System;
using CassandraQueryBuilder;

namespace ExampleProject.Queries
{
    
    public class Select_Queries
    {

        //"SELECT * FROM my_keyspace.users WHERE user_id = ?;"
        internal static readonly String GET_ALL_USER_INFO = new Select()
            .Keyspace(DBVariables.KEYSPACE)
            .Table(DBTables.USERS)
            .WhereColumns(DBColumns.USER_ID)
            .ToString();

        //"SELECT user_id, name FROM my_keyspace.users WHERE user_id = ?;"
        internal static readonly String GET_USER_ID_AND_NAME = new Select()
            .Keyspace(DBVariables.KEYSPACE)
            .Table(DBTables.USERS)
            .SelectColumns(
                DBColumns.USER_ID,
                DBColumns.NAME
            )
            .WhereColumns(DBColumns.USER_ID)
            .ToString();

    }
}
