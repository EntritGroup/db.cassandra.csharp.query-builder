using System;
using CassandraQueryBuilder;

namespace ExampleProject.Queries
{
    public class CreateType_Queries
    {

        //"CREATE TYPE my_keyspace.users (name TEXT, email TEXT);"
        internal static readonly String USER_TYPE = new CreateType()
            .Keyspace(DBVariables.KEYSPACE)
            .Table(DBTables.USERS)
            .Columns(
                DBColumns.NAME,
                DBColumns.EMAIL
            )
            .ToString();

    }
}
