namespace CassandraQueryBuilder.Tests.UT
{
    public class Columns
    {
        internal static readonly Column partitionKey1 = new Column("pk1", ColumnType.TEXT);
        internal static readonly Column partitionKey2 = new Column("pk2", ColumnType.TEXT);
        internal static readonly Column clusteringKey1 = new Column("ck1", ColumnType.TEXT);
        internal static readonly Column clusteringKey2 = new Column("ck2", ColumnType.TEXT);
        internal static readonly Column clusteringKey3 = new Column("ck3", ColumnType.TEXT);
        internal static readonly Column columns1 = new Column("v1", ColumnType.TEXT);
        internal static readonly Column columns2 = new Column("v2", ColumnType.TEXT);
        internal static readonly Column columns3 = new Column("v3", ColumnType.TEXT);
        internal static readonly Column columns4 = new Column("v4", ColumnType.TEXT);
        internal static readonly Column columns_map1 = new Column("vm1", ColumnType.MAP(ColumnType.TEXT, ColumnType.TEXT));
        internal static readonly Column columns_map2 = new Column("vm2", ColumnType.MAP(ColumnType.TEXT, ColumnType.TEXT));
        internal static readonly Column columns_set1 = new Column("vs1", ColumnType.SET(ColumnType.TEXT));
        internal static readonly Column columns_set2 = new Column("vs2", ColumnType.SET(ColumnType.TEXT));
        internal static readonly Column columns_list1 = new Column("vl1", ColumnType.LIST(ColumnType.TEXT));
        internal static readonly Column columns_list2 = new Column("vl2", ColumnType.LIST(ColumnType.TEXT));
        internal static readonly Column columns4_STATIC = new Column("v4", ColumnType.TEXT, true);
        internal static readonly Column columnsCounter1 = new Column("c1", ColumnType.COUNTER);
        internal static readonly Column columnsCounter2 = new Column("c2", ColumnType.COUNTER);
        internal static readonly Column frozen_list1 = new Column("fl1", ColumnType.FROZEN(ColumnType.LIST(ColumnType.TEXT)));
        internal static readonly Column tuple1 = new Column ("t1", ColumnType.TUPLE(new ColumnType[] { ColumnType.TEXT, ColumnType.BOOLEAN } ));
        internal static readonly Column frozen_tuple1 = new Column ("ft1", ColumnType.FROZEN(ColumnType.TUPLE(new ColumnType[] { ColumnType.TEXT, ColumnType.BOOLEAN } )));
        internal static readonly Column list_frozen_tuple1 = new Column ("lft1", ColumnType.LIST(ColumnType.FROZEN(ColumnType.TUPLE(new ColumnType[] { ColumnType.TEXT, ColumnType.BOOLEAN } ))));
        
    }
}
