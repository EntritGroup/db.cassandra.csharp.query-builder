﻿namespace CassandraQueryBuilder.Tests.UT
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
        internal static readonly Column columns_map1 = new Column("vm1", ColumnType.MAP(ColumnType.TEXT, ColumnType.TEXT));
        internal static readonly Column columns_map2 = new Column("vm2", ColumnType.MAP(ColumnType.TEXT, ColumnType.TEXT));
        internal static readonly Column columns_set1 = new Column("vs1", ColumnType.SET(ColumnType.TEXT));
        internal static readonly Column columns_set2 = new Column("vs2", ColumnType.SET(ColumnType.TEXT));
        internal static readonly Column columns_list1 = new Column("vl1", ColumnType.LIST(ColumnType.TEXT));
        internal static readonly Column columns_list2 = new Column("vl2", ColumnType.LIST(ColumnType.TEXT));
        internal static readonly Column columns4_STATIC = new Column("v4", ColumnType.TEXT, true);
        internal static readonly Column columnsCounter = new Column("counter_column_name", ColumnType.COUNTER);
        
    }
}
