using CassandraQueryBuilder;

namespace ExampleProject
{
    public class DBColumns
    {
        internal static readonly Column USER_ID = new Column("user_id", ColumnType.UUID);

        internal static readonly Column NAME = new Column("name", ColumnType.TEXT);
        
        internal static readonly Column EMAIL = new Column("email", ColumnType.TEXT);
        
        internal static readonly Column NAME_STATIC = new Column("name_s", ColumnType.TEXT, true); //Add true in the end if column is static
        
        internal static readonly Column ID_TEXT = new Column("id_t", ColumnType.TEXT);
        
        internal static readonly Column CREATION_DATE_TIMESTAMP = new Column("creation_date_ts", ColumnType.TIMESTAMP);
        
        internal static readonly Column ACTIVE = new Column("active", ColumnType.BOOLEAN);
        
        internal static readonly Column PRIVACY = new Column("privacy", ColumnType.TEXT);
        
        internal static readonly Column USERS_COUNTER = new Column("users_counter", ColumnType.TEXT);
        
        internal static readonly Column COUNT = new Column("count", ColumnType.COUNTER);

    }
}
