using System;
using CassandraQueryBuilder;

namespace ExampleProject.Queries
{
    public class Counter_Queries
    {

        //"UPDATE my_keyspace.users_count SET count = count + 1 WHERE id_t = ?;"
        internal static readonly String INCREASE_NUMBER_OF_USERS_BY_1 = new UpdateCounter()
            .Keyspace(DBVariables.KEYSPACE)
            .Table(DBTables.COUNT_USERS)
            .UpdateColumns(DBColumns.COUNT)
            .IncreaseBy(1)
            .WhereColumns(
                DBColumns.ID_TEXT
            )
            .ToString();

        //"UPDATE my_keyspace.users_count SET count = count + -1 WHERE id_t = ?;"
        internal static readonly String DECREASE_NUMBER_OF_USERS_BY_1 = new UpdateCounter()
            .Keyspace(DBVariables.KEYSPACE)
            .Table(DBTables.COUNT_USERS)
            .UpdateColumns(DBColumns.COUNT)
            .IncreaseBy(-1)
            .WhereColumns(
                DBColumns.ID_TEXT
            )
            .ToString();

        //"UPDATE my_keyspace.users_count SET count = count + ? WHERE id_t = ?;"
        internal static readonly String CHANGE_NUMBER_OF_USERS_BY_LATER_CHOICE = new UpdateCounter()
            .Keyspace(DBVariables.KEYSPACE)
            .Table(DBTables.COUNT_USERS)
            .UpdateColumns(DBColumns.COUNT)
            .WhereColumns(
                DBColumns.ID_TEXT
            )
            .ToString();

    }
}
