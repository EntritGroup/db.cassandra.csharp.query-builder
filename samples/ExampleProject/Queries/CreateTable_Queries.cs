using System;
using CassandraQueryBuilder;

namespace ExampleProject.Queries
{
    public class CreateTable_Queries
    {

        //First table stores all user information
        //"CREATE TABLE my_keyspace.users (user_id UUID, name TEXT, email TEXT, privacy TEXT, active BOOLEAN, creation_date_ts TIMESTAMP, PRIMARY KEY (user_id)) WITH compaction = { 'class' : 'LeveledCompactionStrategy' };"
        internal static readonly String USER_TABLE = new CreateTable()
            .Keyspace(DBVariables.KEYSPACE)
            .Table(DBTables.USERS)
            .PartitionKeys(DBColumns.USER_ID)
            .Columns(
                DBColumns.NAME,
                DBColumns.EMAIL,
                DBColumns.PRIVACY,
                DBColumns.ACTIVE,
                DBColumns.CREATION_DATE_TIMESTAMP
            )
            .CompactionStrategy(CompactionStrategy.LeveledCompactionStrategy)
            .ToString();

        //Second table counts number of users
        //"CREATE TABLE my_keyspace.count_users (users_counter TEXT, count COUNTER, PRIMARY KEY (users_counter)) WITH compaction = { 'class' : 'LeveledCompactionStrategy' };"
        internal static readonly String COUNT_USERS_TABLE = new CreateTable()
            .Keyspace(DBVariables.KEYSPACE)
            .Table(DBTables.COUNT_USERS)
            .PartitionKeys(DBColumns.USERS_COUNTER)
            .Columns(DBColumns.COUNT)
            .CompactionStrategy(CompactionStrategy.LeveledCompactionStrategy)
            .ToString();        

    }
}
