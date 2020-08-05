using System;
using CassandraQueryBuilder;

namespace ExampleProject.Queries
{
    public class Insert_Queries
    {
        //"INSERT INTO my_keyspace.users (user_id, name, email, privacy, active, creation_date_ts) VALUES (?, ?, ?, ?, ?, ?);"
        internal static readonly String ADD_USER = new Insert()
            .Keyspace(DBVariables.KEYSPACE)
            .Table(DBTables.USERS)
            .InsertColumns(
                DBColumns.USER_ID,
                DBColumns.NAME,
                DBColumns.EMAIL,
                DBColumns.PRIVACY,
                DBColumns.ACTIVE,
                DBColumns.CREATION_DATE_TIMESTAMP
            )
            .ToString();

    }
}
